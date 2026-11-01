# Lab10
## Save & Load Implementation
### Player Score
The player score is saved using the ScoreSaver.cs class, which contains SaveScore and LoadScore functions that get called when the player presses S or L respectively. The functions use BinaryFormatter to de/serialize a ScoreData class, which contains the player's current score. The data gets saved to a custom file and path using Application.persistentDataPath() and the file name "score.dat". When the player presses L to load, the player's points populate with the last saved score. 

### Player and Target Positions
The player an target positions are saved using the SavingService.cs class, which contains the SaveGame and LoadGame functions that are called whenever S or L are pressed, respectively. The SaveGame function looks for all objects in the scene that inherit from the ISaveable interface, extracts their save data (such as their SaveID and transform values) into a single object, and converts that object into a string that is stored in a JSON file named "JSONSaveFile.json".
