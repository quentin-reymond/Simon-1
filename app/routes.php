<?php

declare(strict_types=1);

use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\App;

return function (App $app) {

    $app->get('/', function (Request $request, Response $response) {
        $response->getBody()->write('Bienvenue sur l\'API du jeu Simon.');
        return $response;
    });

    // Connexion à la base de données
    $container = $app->getContainer();
    $pdo = new PDO(
        'mysql:host=localhost;dbname=simon_game;charset=utf8mb4',
        'root', // utilisateur MySQL
        '',     // mot de passe MySQL
        [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]
    );

    // ----------- ROUTES POUR JOUEUR -------------

    // Lister tous les joueurs
    $app->get('/joueurs', function (Request $request, Response $response) use ($pdo) {
        $stmt = $pdo->query('SELECT * FROM joueur');
        $joueurs = $stmt->fetchAll(PDO::FETCH_ASSOC);
        $response->getBody()->write(json_encode($joueurs));
        return $response->withHeader('Content-Type', 'application/json');
    });

    // Obtenir un joueur par ID
    $app->get('/joueurs/{id}', function (Request $request, Response $response, array $args) use ($pdo) {
        $stmt = $pdo->prepare('SELECT * FROM joueur WHERE id_joueur = ?');
        $stmt->execute([$args['id']]);
        $joueur = $stmt->fetch(PDO::FETCH_ASSOC);
        if ($joueur) {
            $response->getBody()->write(json_encode($joueur));
        } else {
            $response->getBody()->write(json_encode(['error' => 'Joueur non trouvé']));
            return $response->withStatus(404);
        }
        return $response->withHeader('Content-Type', 'application/json');
    });

    // Créer un nouveau joueur
    $app->post('/joueurs', function (Request $request, Response $response) use ($pdo) {
        $data = $request->getParsedBody();
        $stmt = $pdo->prepare('INSERT INTO joueur (Nom, Date) VALUES (?, NOW())');
        $stmt->execute([$data['nom']]);
        $id = $pdo->lastInsertId();
        $response->getBody()->write(json_encode([
            'id_joueur' => $id,
            'Nom' => $data['nom'],
            'Date' => date('Y-m-d H:i:s')
        ]));
        return $response->withHeader('Content-Type', 'application/json')->withStatus(201);
    });

    // Supprimer un joueur
    $app->delete('/joueurs/{id}', function (Request $request, Response $response, array $args) use ($pdo) {
        $stmt = $pdo->prepare('DELETE FROM joueur WHERE id_joueur = ?');
        $stmt->execute([$args['id']]);
        $response->getBody()->write(json_encode(['message' => 'Joueur supprimé']));
        return $response->withHeader('Content-Type', 'application/json');
    });

    // ----------- ROUTES POUR PARTIES -------------

    // Lister toutes les parties
    $app->get('/parties', function (Request $request, Response $response) use ($pdo) {
        $stmt = $pdo->query('SELECT * FROM parties');
        $parties = $stmt->fetchAll(PDO::FETCH_ASSOC);
        $response->getBody()->write(json_encode($parties));
        return $response->withHeader('Content-Type', 'application/json');
    });

    // Obtenir une partie par ID
    $app->get('/parties/{id}', function (Request $request, Response $response, array $args) use ($pdo) {
        $stmt = $pdo->prepare('SELECT * FROM parties WHERE id_parties = ?');
        $stmt->execute([$args['id']]);
        $partie = $stmt->fetch(PDO::FETCH_ASSOC);
        if ($partie) {
            $response->getBody()->write(json_encode($partie));
        } else {
            $response->getBody()->write(json_encode(['error' => 'Partie non trouvée']));
            return $response->withStatus(404);
        }
        return $response->withHeader('Content-Type', 'application/json');
    });

    // Créer une nouvelle partie
    $app->post('/parties', function (Request $request, Response $response) use ($pdo) {
        $data = $request->getParsedBody();
        $stmt = $pdo->prepare('INSERT INTO parties (id_joueur, Score, Date_partie) VALUES (?, ?, NOW())');
        $stmt->execute([$data['id_joueur'], $data['Score']]);
        $id = $pdo->lastInsertId();
        $response->getBody()->write(json_encode([
            'id_parties' => $id,
            'id_joueur' => $data['id_joueur'],
            'Score' => $data['Score'],
            'Date_partie' => date('Y-m-d H:i:s')
        ]));
        return $response->withHeader('Content-Type', 'application/json')->withStatus(201);
    });

    // Supprimer une partie
    $app->delete('/parties/{id}', function (Request $request, Response $response, array $args) use ($pdo) {
        $stmt = $pdo->prepare('DELETE FROM parties WHERE id_parties = ?');
        $stmt->execute([$args['id']]);
        $response->getBody()->write(json_encode(['message' => 'Partie supprimée']));
        return $response->withHeader('Content-Type', 'application/json');
    });
};
