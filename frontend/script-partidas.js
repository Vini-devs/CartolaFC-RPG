const API_PARTIDAS = "http://localhost:5023/partidas";
const API_TIMES = "http://localhost:5023/times";

function loadPartidasList() {
  fetch(API_PARTIDAS)
    .then((r) => r.json())
    .then((partidas) => {
      const el = document.getElementById("partidas-list");
      if (!el) return;
      if (!partidas.length) {
        el.innerHTML =
          '<div class="alert alert-info">Nenhuma partida cadastrada.</div>';
        return;
      }
      el.innerHTML = `<ul class="list-group">${partidas
        .map(
          (p) => `<li class='list-group-item d-flex justify-content-between align-items-center'>
        <span>
          ${p.timeCasaNome} ${p.golsCasa} x ${p.golsVisitante} ${p.timeVisitanteNome}
          <small class="text-muted ms-2">${new Date(p.dataHora).toLocaleString()}</small>
        </span>
        <span>
          <a href='editar-partida.html?id=${p.id}' class='btn btn-sm btn-warning me-2'>Editar</a>
          <button class='btn btn-sm btn-danger' onclick='removerPartida(${p.id})'>Remover</button>
        </span>
      </li>`
        )
        .join("")}</ul>`;
    });
}

function removerPartida(id) {
  if (!confirm("Tem certeza que deseja remover esta partida?")) return;
  fetch(`${API_PARTIDAS}/${id}`, { method: "DELETE" }).then(() => loadPartidasList());
}

function loadCriarPartida() {
  // Preencher selects de times
  fetch(API_TIMES)
    .then((r) => r.json())
    .then((times) => {
      const casaSelect = document.getElementById("timeCasaId");
      const visitanteSelect = document.getElementById("timeVisitanteId");
      times.forEach((t) => {
        const opt1 = document.createElement("option");
        opt1.value = t.id;
        opt1.textContent = t.nome;
        casaSelect.appendChild(opt1);

        const opt2 = document.createElement("option");
        opt2.value = t.id;
        opt2.textContent = t.nome;
        visitanteSelect.appendChild(opt2);
      });
    });

  document.getElementById("form-criar-partida").onsubmit = function (e) {
    e.preventDefault();
    const timeCasaId = parseInt(this.timeCasaId.value, 10);
    const timeVisitanteId = parseInt(this.timeVisitanteId.value, 10);
    const golsCasa = parseInt(this.golsCasa.value, 10);
    const golsVisitante = parseInt(this.golsVisitante.value, 10);
    const dataHora = this.dataHora.value;
    fetch(API_PARTIDAS, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        timeCasaId,
        timeVisitanteId,
        golsCasa,
        golsVisitante,
        dataHora,
      }),
    }).then(() => (window.location = "partidas.html"));
  };
}

function loadEditarPartida() {
  const params = new URLSearchParams(window.location.search);
  const id = params.get("id");
  let partida;
  fetch(`${API_PARTIDAS}/${id}`)
    .then((r) => r.json())
    .then((p) => {
      partida = p;
      return fetch(API_TIMES).then((r) => r.json());
    })
    .then((times) => {
      const casaSelect = document.getElementById("timeCasaId");
      const visitanteSelect = document.getElementById("timeVisitanteId");
      times.forEach((t) => {
        const opt1 = document.createElement("option");
        opt1.value = t.id;
        opt1.textContent = t.nome;
        if (t.id === partida.timeCasaId) opt1.selected = true;
        casaSelect.appendChild(opt1);

        const opt2 = document.createElement("option");
        opt2.value = t.id;
        opt2.textContent = t.nome;
        if (t.id === partida.timeVisitanteId) opt2.selected = true;
        visitanteSelect.appendChild(opt2);
      });
      document.getElementById("golsCasa").value = partida.golsCasa;
      document.getElementById("golsVisitante").value = partida.golsVisitante;
      document.getElementById("dataHora").value = partida.dataHora?.slice(0, 16) || "";
    });

  document.getElementById("form-editar-partida").onsubmit = function (e) {
    e.preventDefault();
    const timeCasaId = parseInt(this.timeCasaId.value, 10);
    const timeVisitanteId = parseInt(this.timeVisitanteId.value, 10);
    const golsCasa = parseInt(this.golsCasa.value, 10);
    const golsVisitante = parseInt(this.golsVisitante.value, 10);
    const dataHora = this.dataHora.value;
    fetch(`${API_PARTIDAS}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        timeCasaId,
        timeVisitanteId,
        golsCasa,
        golsVisitante,
        dataHora,
      }),
    }).then(() => (window.location = "partidas.html"));
  };
}
