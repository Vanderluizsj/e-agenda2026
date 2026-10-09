const themeToggle = document.getElementById("themeToggle");
const themeIcon = document.getElementById("themeIcon");

function aplicarTema(tema) {
    document.documentElement.setAttribute("data-bs-theme", tema);

    localStorage.setItem("theme", tema);

    themeIcon.className = tema === "dark"
        ? "bi bi-sun"
        : "bi bi-moon";
}

const temaSalvo = localStorage.getItem("theme");

if (temaSalvo) {
    aplicarTema(temaSalvo);
}

themeToggle.addEventListener("click", () => {
    const temaAtual = document.documentElement.getAttribute("data-bs-theme");

    aplicarTema(temaAtual === "dark" ? "light" : "dark");
});