const API_TORNEIOS = "http://localhost:5023/torneios";

function loadTorneiosList() {
  fetch(API_TORNEIOS)
    .then((r) => r.json())
    .then((torneios) => {
      const el = document.getElementById("torneios-list");
      if (!el) return;
      if (!torneios.length) {
        el.innerHTML =
          '<div class="alert alert-info">Nenhum torneio cadastrado.</div>';
        return;
      }
      el.innerHTML = `<ul class="list-group">${torneios
        .map(
          (
            t
          ) => `<li class='list-group-item d-flex justify-content-between align-items-center'>
        <span>
          <strong>${t.nome}</strong>
        </span>
        <span>
          <a href='editar-torneio.html?id=${t.id}' class='btn btn-sm btn-warning me-2'>Editar</a>
          <button class='btn btn-sm btn-danger' onclick='removerTorneio(${t.id})'>Remover</button>
        </span>
      </li>`
        )
        .join("")}</ul>`;
    });
}

function removerTorneio(id) {
  if (!confirm("Tem certeza que deseja remover este torneio?")) return;
  fetch(`${API_TORNEIOS}/${id}`, { method: "DELETE" }).then(() =>
    loadTorneiosList()
  );
}

function loadCriarTorneio() {
  document.getElementById("form-criar-torneio").onsubmit = function (e) {
    e.preventDefault();
    const nome = this.nome.value;
    fetch(API_TORNEIOS, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nome,
        partidaIds: [],
      }),
    }).then(() => {
      alert(`Torneio '${nome}' salvo com sucesso!`);
      window.location = "torneios.html";
    });
  };
}

function loadEditarTorneio() {
  const params = new URLSearchParams(window.location.search);
  const id = params.get("id");
  fetch(`${API_TORNEIOS}/${id}`)
    .then((r) => r.json())
    .then((torneio) => {
      document.getElementById("nome").value = torneio.nome || "";
      document.getElementById("form-editar-torneio").onsubmit = function (e) {
        e.preventDefault();
        const nome = this.nome.value;
        fetch(`${API_TORNEIOS}/${id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            nome,
            partidaIds: torneio.partidaIds || [],
          }),
        }).then(() => (window.location = "torneios.html"));
      };
    });
}
