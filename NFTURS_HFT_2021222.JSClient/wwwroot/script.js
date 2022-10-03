let games = [];
let connection = null;
let gameIdToUpdate = -1;
getData();
setupSignalR();


async function getData() {
    await fetch('http://localhost:32095/Game')
        .then(x => x.json())
        .then(y => {
            games = y;
            console.log(games);
            display();
        });
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:32095/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GameCreated", (user, message) => {
        getData();
    });

    connection.on("GameDeleted", (user, message) => {
        getData();
    });

    connection.on("GameUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function display() {

    document.getElementById('gamedata').innerHTML = "";

    games.forEach(g => {
        document.getElementById('gamedata')
            .innerHTML += "<tr><td>" + g.gameId + "</td><td>"
            + g.name + "</td><td>"
            + g.gameRating
            + "</td><td>" + `<button type="button" onclick="remove(${g.gameId})">Delete</button>`
            + `<button type="button" onclick="showUpdate(${g.gameId})">Update</button>`
            + "</td></tr>";

        console.log(g.name);
    });
}

function showUpdate(id) {
    document.getElementById('gamenametoupdate').value = games.find(g => g['gameId'] == id)['name'];
    document.getElementById('gameratingtoupdate').value = games.find(g => g['gameId'] == id)['gameRating'];
    document.getElementById('updateformdiv').style.display = 'flex';

    gameIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';

    let gameName = document.getElementById('gamenametoupdate').value;
    let gameRating = document.getElementById('gameratingtoupdate').value;

    fetch('http://localhost:32095/Game', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        //body: JSON.stringify({ name: gameName, publisher.name: gamePublisher, gameRating: gameRating }),
        body: JSON.stringify({ gameId: gameIdToUpdate, name: gameName, gameRating: gameRating })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function remove(id) {
    fetch('http://localhost:32095/Game/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let gameName = document.getElementById('gamename').value;
    let gameRating = document.getElementById('gamerating').value;

    fetch('http://localhost:32095/Game', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        //body: JSON.stringify({ name: gameName, publisher.name: gamePublisher, gameRating: gameRating }),
        body: JSON.stringify({ name: gameName, gameRating: gameRating })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}