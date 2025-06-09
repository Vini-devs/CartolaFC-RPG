const API_TORNEIOS = "http://localhost:5023/torneios";
const API_PARTIDAS = "http://localhost:5023/partidas";

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
          <a href='consultar-torneio.html?id=${t.id}'>
            <strong>${t.nome}</strong>
          </a>
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
  fetch(API_PARTIDAS)
    .then((r) => r.json())
    .then((partidas) => {
      for (let i = 1; i <= 8; i++) {
        const select = document.getElementById(`partida${i}Id`);
        if (!select) continue;
        select.innerHTML =
          '<option value="">Selecione a partida ' + i + "</option>";
        partidas.forEach((p) => {
          select.innerHTML += `<option value="${p.id}">${new Date(
            p.data
          ).toLocaleString("pt-BR")} - ${
            p.timeCasa ? p.timeCasa.nome || p.timeCasa : p.timeIdCasa
          } x ${
            p.timeFora ? p.timeFora.nome || p.timeFora : p.timeIdFora
          }</option>`;
        });
      }
    });
  document.getElementById("form-criar-torneio").onsubmit = function (e) {
    e.preventDefault();
    const nome = this.nome.value;
    // Coletar os valores dos selects de partidas
    const partidaIds = [];
    for (let i = 1; i <= 8; i++) {
      const val = parseInt(this[`partida${i}Id`]?.value);
      console.log(`Partida ${i} selecionada:`, val);
      if (val) partidaIds.push(val);
    }
    // Remover duplicatas
    const partidaIdsUnicos = [...new Set(partidaIds)];
    fetch(API_TORNEIOS, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nome,
        partidaIds: partidaIdsUnicos,
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
  Promise.all([
    fetch(`${API_TORNEIOS}/${id}`).then((r) => r.json()),
    fetch(API_PARTIDAS).then((r) => r.json()),
  ]).then(([torneio, partidas]) => {
    document.getElementById("nome").value = torneio.nome || "";

    for (let i = 1; i <= 8; i++) {
      const select = document.getElementById(`partida${i}Id`);
      if (!select) continue;

      select.innerHTML =
        '<option value="">Selecione a partida ' + i + "</option>";

      partidas.forEach((p) => {
        const selected =
          torneio.partidaIds && torneio.partidaIds[i - 1] == p.id
            ? "selected"
            : "";
        select.innerHTML += `<option value="${p.id}" ${selected}>${new Date(
          p.data
        ).toLocaleString("pt-BR")} - ${
          p.timeCasa ? p.timeCasa.nome || p.timeCasa : p.timeIdCasa
        } x ${
          p.timeFora ? p.timeFora.nome || p.timeFora : p.timeIdFora
        }</option>`;
      });
    }

    document.getElementById("form-editar-torneio").onsubmit = function (e) {
      e.preventDefault();
      const nome = this.nome.value;
      // Coletar os valores dos selects de partidas
      const partidaIds = [];
      for (let i = 1; i <= 8; i++) {
        const val = parseInt(this[`partida${i}Id`]?.value);
        if (val) partidaIds.push(val);
      }
      // Remover duplicatas
      const partidaIdsUnicos = [...new Set(partidaIds)];

      fetch(`${API_TORNEIOS}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          nome,
          partidaIds: partidaIdsUnicos,
        }),
      }).then(() => {
        window.location = "torneios.html";
      });
    };
  });
}

// Função para exibir partidas do torneio
async function loadPartidasDoTorneio() {
  const torneioId = getQueryParam("id");
  if (!torneioId) {
    document.getElementById("partidas-lista").innerHTML =
      '<div class="alert alert-danger">Torneio não especificado.</div>';
    return;
  }
  try {
    // Buscar nome do torneio
    const torneioResp = await fetch(`${API_TORNEIOS}/${torneioId}`);
    const torneio = await torneioResp.json();
    document.getElementById("torneio-nome").textContent =
      torneio.nome || "Torneio";

    // Buscar partidas individualmente
    const partidaIds = torneio.partidaIds || [];
    if (!partidaIds.length) {
      document.getElementById("partidas-lista").innerHTML =
        '<div class="alert alert-info">Nenhuma partida cadastrada para este torneio.</div>';
      return;
    }
    // Buscar todas as partidas em paralelo
    const partidas = await Promise.all(
      partidaIds.map((id) =>
        fetch(`${API_PARTIDAS}/${id}`).then((r) => r.json())
      )
    );

    // Ordenar por data
    const partidasOrdenadas = partidas.sort(
      (a, b) => new Date(a.data) - new Date(b.data)
    );
    let html = '<ul class="list-group">';
    partidasOrdenadas.forEach((p) => {
      html += `<li class="list-group-item d-flex justify-content-between align-items-center">
              <span>${new Date(p.data).toLocaleString("pt-BR")} -
              ${p.timeCasa ? p.timeCasa.nome || p.timeCasa : p.timeIdCasa} x 
              ${p.timeFora ? p.timeFora.nome || p.timeFora : p.timeIdFora}
              </span>
            </li>`;
    });
    html += "</ul>";
    document.getElementById("partidas-lista").innerHTML = html;
  } catch (e) {
    document.getElementById("partidas-lista").innerHTML =
      '<div class="alert alert-danger">Erro ao carregar partidas.</div>';
  }
}
