Anmerkungen zum Code und Projektstand:

- Ab und zu Kollisionsbugs z.B. Spieler kann durch Wände gehen, falls nahe stehende Collider ungünstig stehen -> Tritt meist mit MehshCollider auf
- In diesem Code Stand sind 4 Rätsel -> aber durch entkoppelte PuzzleLogik schnell und einfach erweiterbar
- Rendering von Puzzle-Übersicht nicht entkoppelt, dafür hat leider die Zeit gefehlt
- Interactables (siehe Buttons am Eingang der Kirche) ermöglichen auch noch komplexere Rätsel zu gestalten (z.B. Kompinationsrätsel mit Hebeln).
- Die ersten Interactables (Buttons, Schalter und Hebel) verwenden noch keine Animationen (wird noch alles per skript animiert) hier bietet es sich an auf Unity-Animationen umzustellen
 Dies wurde z.B im Glockenturm für die Seile so gemacht

- Items müssen im Item-Layer sein und einen Rigidbody besitzen, sonst werden diese nicht von den Aufhebe-Collideren im Character erkannt
- Handling von Interactables und Items getrennt
- Events leider nicht ganz entkoppelt, manchmal sind Beziehungen zwischen den Skripten gesetzt (hätten durch Events entkoppelt werden können),
 das macht es teilweise schweirig auf Anhieb nachzuvollziehen wie alles Zusammenhängt.
- Der Spieler kann Objekte in zwei Positionen tragen in der Hand oder vor sich mit beiden Händen. 
  Es kann ein Offset angeben werden z.b für kleinere Kisten
  Um den Offset zu bestimmen ist es am einfachsten den Gegenstand einmal aufzuheben und im Editor zu Pausieren 
  -> dann Gegenstand an Gewünschte Position verschieben und die Position notieren
  -> Spiel beenden und notierte Position unter "Offset eintragen"
- Interactables können bei interaction Events ausführen, hier wäre noch ein weiterer Eventtyp ähnlich dem von UI-Buttons nützlich,
 mit welchen man von beliebigen Skripten beliebige funktionen aufrufen kann.
