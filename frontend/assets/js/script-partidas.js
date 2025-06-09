const API_PARTIDAS = "http://localhost:5023/partidas";
const API_TIMES = "http://localhost:5023/times";

function loadPartidasList() {
  Promise.all([
    fetch(API_PARTIDAS).then((r) => r.json()),
    fetch(API_TIMES).then((r) => r.json()),
  ]).then(([partidas, times]) => {
    const el = document.getElementById("partidas-list");
    if (!el) return;
    if (!partidas.length) {
      el.innerHTML =
        '<div class="alert alert-info">Nenhuma partida cadastrada.</div>';
      return;
    }
    el.innerHTML = `<ul class="list-group">${partidas
      .map((p) => {
        const timeCasa = times.find((t) => t.id === p.timeIdCasa);
        const timeFora = times.find((t) => t.id === p.timeIdFora);
        return `<li class='list-group-item d-flex justify-content-between align-items-center'>
        <span>
          ${timeCasa ? timeCasa.nome : p.timeIdCasa} ${p.placarCasa} x ${
          p.placarFora
        } ${timeFora ? timeFora.nome : p.timeIdFora}
          <small class="text-muted ms-2">${new Date(
            p.data
          ).toLocaleString()}</small>
        </span>
        <span>
          <a href='editar-partida.html?id=${
            p.id
          }' class='btn btn-sm btn-warning me-2'>Editar</a>
          <button class='btn btn-sm btn-danger' onclick='removerPartida(${
            p.id
          })'>Remover</button>
        </span>
      </li>`;
      })
      .join("")}</ul>`;
  });
}

function removerPartida(id) {
  if (!confirm("Tem certeza que deseja remover esta partida?")) return;
  fetch(`${API_PARTIDAS}/${id}`, { method: "DELETE" }).then(() =>
    loadPartidasList()
  );
}

function loadCriarPartida() {
  // Preencher selects de times
  fetch(API_TIMES)
    .then((r) => r.json())
    .then((times) => {
      const casaSelect = document.getElementById("timeIdCasa");
      const foraSelect = document.getElementById("timeIdFora");
      times.forEach((t) => {
        const opt1 = document.createElement("option");
        opt1.value = t.id;
        opt1.textContent = t.nome;
        casaSelect.appendChild(opt1);

        const opt2 = document.createElement("option");
        opt2.value = t.id;
        opt2.textContent = t.nome;
        foraSelect.appendChild(opt2);
      });
    });

  document.getElementById("form-criar-partida").onsubmit = function (e) {
    e.preventDefault();
    const timeIdCasa = parseInt(this.timeIdCasa.value, 10);
    const timeIdFora = parseInt(this.timeIdFora.value, 10);
    const placarCasa = parseInt(this.placarCasa.value, 10);
    const placarFora = parseInt(this.placarFora.value, 10);
    const data = this.data.value;
    // Buscar nomes dos times para o alerta
    Promise.all([
      fetch(`${API_TIMES}/${timeIdCasa}`).then((r) => r.json()),
      fetch(`${API_TIMES}/${timeIdFora}`).then((r) => r.json()),
    ])
      .then(([casa, fora]) => {
        fetch(API_PARTIDAS, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            timeIdCasa,
            timeIdFora,
            placarCasa,
            placarFora,
            data,
          }),
        }).then(() => {
          alert(`Partida '${casa.nome} x ${fora.nome}' salva com sucesso!`);
          window.location = "partidas.html";
        });
      })
      .catch((error) => console.error("Erro ao salvar partida:", error));
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
      const casaSelect = document.getElementById("timeIdCasa");
      const foraSelect = document.getElementById("timeIdFora");
      times.forEach((t) => {
        const opt1 = document.createElement("option");
        opt1.value = t.id;
        opt1.textContent = t.nome;
        if (t.id === partida.timeIdCasa) opt1.selected = true;
        casaSelect.appendChild(opt1);

        const opt2 = document.createElement("option");
        opt2.value = t.id;
        opt2.textContent = t.nome;
        if (t.id === partida.timeIdFora) opt2.selected = true;
        foraSelect.appendChild(opt2);
      });
      document.getElementById("placarCasa").value = partida.placarCasa;
      document.getElementById("placarFora").value = partida.placarFora;
      document.getElementById("data").value = partida.data?.slice(0, 16) || "";
    });

  document.getElementById("form-editar-partida").onsubmit = function (e) {
    e.preventDefault();
    const timeIdCasa = parseInt(this.timeIdCasa.value, 10);
    const timeIdFora = parseInt(this.timeIdFora.value, 10);
    const placarCasa = parseInt(this.placarCasa.value, 10);
    const placarFora = parseInt(this.placarFora.value, 10);
    const data = this.data.value;
    fetch(`${API_PARTIDAS}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        timeIdCasa,
        timeIdFora,
        placarCasa,
        placarFora,
        data,
      }),
    }).then(() => (window.location = "partidas.html"));
  };
}
