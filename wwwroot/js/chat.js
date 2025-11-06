const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.on("ReceiveMessage", (numero, mensaje) => {
    const li = document.createElement("li");
    li.classList.add("list-group-item");
    li.textContent = `📲 ${numero}: ${mensaje}`;
    document.getElementById("mensajes").prepend(li);
});

connection.start()
    .then(() => console.log("✅ Conectado al servidor SignalR"))
    .catch(err => console.error("❌ Error SignalR:", err));
