//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

let tarefasCarregadas = [];

document.addEventListener("DOMContentLoaded", function () {
    carregarTarefas();

    document
    .getElementById("btnNovaTarefa")
    .addEventListener("click", abrirModalNovaTarefa);

    document
    .getElementById("btnSalvarTarefa")
    .addEventListener("click", salvarTarefa);

    document
        .getElementById("pesquisaTarefas")
        .addEventListener("input", atualizarTabela);

    document
        .getElementById("filtroStatus")
        .addEventListener("change", atualizarTabela);
});

async function carregarTarefas() {
    const resposta = await fetch("/api/tarefas");

    if (!resposta.ok) {
        console.error("Erro ao carregar tarefas.");
        return;
    }

    tarefasCarregadas = await resposta.json();

    atualizarIndicadores();
    atualizarTabela();
}

function atualizarIndicadores() {
    const total = tarefasCarregadas.length;

    const pendentes = tarefasCarregadas.filter(
        tarefa => tarefa.status === 0
    ).length;

    const emAndamento = tarefasCarregadas.filter(
        tarefa => tarefa.status === 1
    ).length;

    const concluidas = tarefasCarregadas.filter(
        tarefa => tarefa.status === 2
    ).length;

    document.getElementById("totalTarefas").textContent = total;
    document.getElementById("tarefasPendentes").textContent = pendentes;
    document.getElementById("tarefasEmAndamento").textContent = emAndamento;
    document.getElementById("tarefasConcluidas").textContent = concluidas;
}

function atualizarTabela() {
    const pesquisa = document
        .getElementById("pesquisaTarefas")
        .value
        .toLowerCase();

    const statusSelecionado = document
        .getElementById("filtroStatus")
        .value;

    const tarefasFiltradas = tarefasCarregadas.filter(tarefa => {
        const correspondePesquisa =
            tarefa.titulo.toLowerCase().includes(pesquisa) ||
            tarefa.descricao.toLowerCase().includes(pesquisa);

        const correspondeStatus =
            statusSelecionado === "" ||
            tarefa.status.toString() === statusSelecionado;

        return correspondePesquisa && correspondeStatus;
    });

    const listaTarefas = document.getElementById("listaTarefas");

    listaTarefas.innerHTML = "";

    if (tarefasFiltradas.length === 0) {
        const linha = document.createElement("tr");
        const coluna = document.createElement("td");

        coluna.colSpan = 6;
        coluna.textContent = "Nenhuma tarefa encontrada.";
        coluna.classList.add("text-center", "text-muted", "py-4");

        linha.appendChild(coluna);
        listaTarefas.appendChild(linha);

        return;
    }

    tarefasFiltradas.forEach(tarefa => {
        const linha = document.createElement("tr");

        const colunaTitulo = document.createElement("td");
        colunaTitulo.textContent = tarefa.titulo;

        const colunaVencimento = document.createElement("td");
        colunaVencimento.textContent = tarefa.dataVencimento
            ? new Date(tarefa.dataVencimento).toLocaleDateString("pt-BR")
            : "-";

        const colunaStatus = document.createElement("td");
        colunaStatus.textContent = obterNomeStatus(tarefa.status);

        const colunaPrioridade = document.createElement("td");
        colunaPrioridade.textContent = obterNomePrioridade(tarefa.prioridade);

        const colunaCriacao = document.createElement("td");
        colunaCriacao.textContent = new Date(
            tarefa.dataCriacao
        ).toLocaleDateString("pt-BR");

        const colunaAcoes = document.createElement("td");

        const linkDetalhes = document.createElement("a");
        linkDetalhes.href = `/Tarefas/Details/${tarefa.id}`;
        linkDetalhes.textContent = "Detalhes";
        linkDetalhes.classList.add("btn", "btn-sm", "btn-outline-primary");

        colunaAcoes.appendChild(linkDetalhes);

        const btnEditar = document.createElement("button");

        btnEditar.textContent = "Editar";
        btnEditar.classList.add("btn", "btn-sm", "btn-outline-warning", "ms-2");

        btnEditar.addEventListener("click", function () {
            abrirModalEdicao(tarefa);
        });

        colunaAcoes.appendChild(btnEditar);

        const btnExcluir = document.createElement("button");

        btnExcluir.textContent = "Excluir";
        btnExcluir.classList.add("btn", "btn-sm", "btn-outline-danger", "ms-2");

        btnExcluir.addEventListener("click", function () {
            excluirTarefa(tarefa.id);
        });

        colunaAcoes.appendChild(btnExcluir);

        linha.appendChild(colunaTitulo);
        linha.appendChild(colunaVencimento);
        linha.appendChild(colunaStatus);
        linha.appendChild(colunaPrioridade);
        linha.appendChild(colunaCriacao);
        linha.appendChild(colunaAcoes);

        listaTarefas.appendChild(linha);
    });
}

function obterNomeStatus(status) {
    switch (status) {
        case 0:
            return "Pendente";
        case 1:
            return "Em andamento";
        case 2:
            return "Concluída";
        default:
            return "Desconhecido";
    }
}

function obterNomePrioridade(prioridade) {
    switch (prioridade) {
        case 0:
            return "Baixa";
        case 1:
            return "Média";
        case 2:
            return "Alta";
        default:
            return "Desconhecida";
    }
}

function abrirModalNovaTarefa() {
    document.getElementById("formTarefa").reset();

    document.getElementById("tarefaId").value = "";
    document.getElementById("tituloModal").textContent = "Nova tarefa";

    const modal = new bootstrap.Modal(
        document.getElementById("modalTarefa")
    );

    modal.show();
}

async function salvarTarefa() {
    const id = document.getElementById("tarefaId").value;

    const tarefa = {
        titulo: document.getElementById("titulo").value,
        descricao: document.getElementById("descricao").value,
        dataVencimento: document.getElementById("dataVencimento").value,
        status: Number(document.getElementById("status").value),
        prioridade: Number(document.getElementById("prioridade").value)
    };

    let url = "/api/tarefas";
    let metodo = "POST";

    if (id !== "") {
        url += `/${id}`;
        metodo = "PUT";
    }

    const resposta = await fetch(url, {
        method: metodo,
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(tarefa)
    });

    if (!resposta.ok) {
        alert("Não foi possível salvar a tarefa.");
        return;
    }

    bootstrap.Modal
        .getInstance(document.getElementById("modalTarefa"))
        .hide();

    await carregarTarefas();
}

function abrirModalEdicao(tarefa) {
    document.getElementById("tarefaId").value = tarefa.id;
    document.getElementById("titulo").value = tarefa.titulo;
    document.getElementById("descricao").value = tarefa.descricao;
    document.getElementById("status").value = tarefa.status;
    document.getElementById("prioridade").value = tarefa.prioridade;

    const data = new Date(tarefa.dataVencimento);

    const ano = data.getFullYear();
    const mes = String(data.getMonth() + 1).padStart(2, "0");
    const dia = String(data.getDate()).padStart(2, "0");
    const hora = String(data.getHours()).padStart(2, "0");
    const minuto = String(data.getMinutes()).padStart(2, "0");

    document.getElementById("dataVencimento").value =
        `${ano}-${mes}-${dia}T${hora}:${minuto}`;

    document.getElementById("tituloModal").textContent = "Editar tarefa";

    const modal = new bootstrap.Modal(
        document.getElementById("modalTarefa")
    );

    modal.show();
}

async function excluirTarefa(id) {
    const confirmar = confirm(
        "Tem certeza de que deseja excluir esta tarefa?"
    );

    if (!confirmar) {
        return;
    }

    const resposta = await fetch(`/api/tarefas/${id}`, {
        method: "DELETE"
    });

    if (!resposta.ok) {
        alert("Não foi possível excluir a tarefa.");
        return;
    }
    
    await carregarTarefas();

    alert("Tarefa excluída com sucesso!");
}