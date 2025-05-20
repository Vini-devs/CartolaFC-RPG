const API_JOGADORES = "http://localhost:5023/jogadores";

function loadJogadoresList() {
  fetch(API_JOGADORES)
    .then((r) => r.json())
    .then((jogadores) => {
      const el = document.getElementById("jogadores-list");
      if (!el) return;
      if (!jogadores.length) {
        el.innerHTML =
          '<div class="alert alert-info">Nenhum jogador cadastrado.</div>';
        return;
      }
      el.innerHTML = `<ul class="list-group">${jogadores
        .map(
          (j) => `<li class='list-group-item d-flex justify-content-between align-items-center'>
        <span>
          <img src="${j.urlFoto || ''}" alt="" style="width:32px;height:32px;object-fit:cover;border-radius:50%;margin-right:8px;">
          ${j.nome} (${j.posicao}, #${j.numero})
        </span>
        <span>
          <a href='editar-jogador.html?id=${j.id}' class='btn btn-sm btn-warning me-2'>Editar</a>
          <button class='btn btn-sm btn-danger' onclick='removerJogador(${j.id})'>Remover</button>
        </span>
      </li>`
        )
        .join("")}</ul>`;
    });
}

function removerJogador(id) {
  if (!confirm("Tem certeza que deseja remover este jogador?")) return;
  fetch(`${API_JOGADORES}/${id}`, { method: "DELETE" }).then(() => loadJogadoresList());
}

function loadCriarJogador() {
  document.getElementById("form-criar-jogador").onsubmit = function (e) {
    e.preventDefault();
    const nome = this.nome.value;
    const posicao = this.posicao.value;
    const numero = parseInt(this.numero.value, 10);
    const urlFoto = this.urlFoto.value;
    fetch(API_JOGADORES, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nome,
        posicao,
        numero,
        urlFoto,
      }),
    }).then(() => (window.location = "jogadores.html"));
  };
}

function loadEditarJogador() {
  const params = new URLSearchParams(window.location.search);
  const id = params.get("id");
  fetch(`${API_JOGADORES}/${id}`)
    .then((r) => r.json())
    .then((jogador) => {
      document.getElementById("nome").value = jogador.nome;
      document.getElementById("posicao").value = jogador.posicao || "";
      document.getElementById("numero").value = jogador.numero || "";
      document.getElementById("urlFoto").value = jogador.urlFoto || "";
      document.getElementById("form-editar-jogador").onsubmit = function (e) {
        e.preventDefault();
        const nome = this.nome.value;
        const posicao = this.posicao.value;
        const numero = parseInt(this.numero.value, 10);
        const urlFoto = this.urlFoto.value;
        fetch(`${API_JOGADORES}/${id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            nome,
            posicao,
            numero,
            urlFoto,
          }),
        }).then(() => (window.location = "jogadores.html"));
      };
    });
}
