-> NPC2

=== NPC2 ===
...

Preguntaré on és l'estació a aquest senyor... 

Hola, què tal soc NPC2?

Home: Hola, noi

Com puc anar a l'estació?

Home: Has d'agafar el carrer de la dreta.

    + [Uno]
        ->escollit("Uno")
    + [Dos]
        ->escollit("Dos")
    + [Tres]
        ->escollit("Tres")
    
=== escollit(direccio) ===
Has escollit {direccio}!
    
-> END