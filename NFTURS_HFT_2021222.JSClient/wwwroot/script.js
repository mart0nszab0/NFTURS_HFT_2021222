let games = [];

fetch('http://localhost:32095/Game')
    .then(x => x.json())
    .then(y => {
        games = y;
        console.log(games);
        display();
        });

function display() {
    games.forEach(g => {
        document.getElementById('gamedata')
            .innerHTML += "<tr><td>" + g.gameId + "</td><td>"
        + g.name + "</td><td>"
        + g.publisher.name + "</td><td>"
        + g.gameRating + "</td></tr>";

        console.log(g.name);
    });
}