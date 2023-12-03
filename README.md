1) La fenêtre principale s'instancie :
    - Elle crée une instance du UserControl LogView affectée comme propriété public de la classe
        1a)
        - LogView instancie un LogController qui le prend en reference et se l'affecte en tant que propriété private

    - Elle crée une instance du UserControl MenuView affectée comme propriété public de la classe
    
    - Elle crée une instance du Controller de connexion, affecté comme propriété private
        1b) 
        - Le ConnexionController prend une reference de la MainWindow qui l'a instancié comme Propriété private
        - Il affecte le clic des deux UserControls de MainWindow à ses méthodes de Connexion / Déconnexion

2) L'utilisateur clique sur le bouton de connexion :
    - Le ConnexionController déclenche son HandleConnexion().
        2a)
	- 




à faire :
Mieux checker le pseudo + mdp lors de la création