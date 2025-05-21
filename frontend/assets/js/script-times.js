const API_BASE = "http://localhost:5023/times";

function loadTimesList() {
  fetch(API_BASE)
    .then((r) => r.json())
    .then((times) => {
      const el = document.getElementById("times-list");
      if (!el) return;
      if (!times.length) {
        el.innerHTML =
          '<div class="alert alert-info">Nenhum time cadastrado.</div>';
        return;
      }
      el.innerHTML = `<ul class="list-group">${times
        .map(
          (
            t
          ) => `<li class='list-group-item d-flex justify-content-between align-items-center'>
        <span>${t.nome}</span>
        <span>
          <a href='editar-time.html?id=${t.id}' class='btn btn-sm btn-warning me-2'>Editar</a>
          <button class='btn btn-sm btn-danger' onclick='removerTime(${t.id})'>Remover</button>
        </span>
      </li>`
        )
        .join("")}</ul>`;
    });
}

function removerTime(id) {
  if (!confirm("Tem certeza que deseja remover este time?")) return;
  fetch(`${API_BASE}/${id}`, { method: "DELETE" }).then(() => loadTimesList());
}

function loadCriarTime() {
  document.getElementById("form-criar-time").onsubmit = function (e) {
    e.preventDefault();
    const nome = this.nome.value;
    const sigla = this.sigla.value;
    fetch(API_BASE, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nome,
        sigla,
        jogadorIds: [],
      }),
    }).then(() => (window.location = "times.html"));
  };
}

function loadEditarTime() {
  const params = new URLSearchParams(window.location.search);
  const id = params.get("id");
  fetch(`${API_BASE}/${id}`)
    .then((r) => r.json())
    .then((time) => {
      document.getElementById("nome").value = time.nome || "";
      document.getElementById("sigla").value = time.sigla || "";
      document.getElementById("form-editar-time").onsubmit = function (e) {
        e.preventDefault();
        const nome = this.nome.value;
        const sigla = this.sigla.value;
        fetch(`${API_BASE}/${id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            nome,
            sigla,
            jogadorIds: time.jogadorIds || [],
          }),
        }).then(() => (window.location = "times.html"));
      };
    });
}
