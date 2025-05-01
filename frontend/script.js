fetch("http://localhost:5023/times")
  .then((response) => response.json())
  .then((data) => {
    const content = document.getElementById("content");
    data.forEach((time) => {
      const div = document.createElement("div");
      div.textContent = `${time.nome} (${time.sigla})`;
      content.appendChild(div);
    });
  });
