# Lab10
## Save & Load Implementation
### Player Score
The player score is saved using the ScoreSaver.cs class, which contains SaveScore and LoadScore functions that get called when the player presses S or L respectively. The functions use BinaryFormatter to de/serialize a ScoreData class, which contains the player's current score. The data gets saved to a custom file and path using Application.persistentDataPath() and the file name "score.dat". When the player presses L to load, the player's points populate with the last saved score. 
