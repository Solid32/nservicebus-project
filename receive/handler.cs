using NServiceBus;
using System.Data.SQLite;
using System.Text.RegularExpressions;

public class MyMessageHandler : IHandleMessages<MyMessage>
{
    static string connectionString = "Data Source=test.db;Version=3;";

    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        // Diviser le message en parties en utilisant la virgule comme séparateur
        var messageParts = message.Data.Split(',');
        var abilities = Regex.Matches(messageParts[4], @"[a-zA-Z\s]+")
                             .Cast<Match>()
                             .Select(m => m.Value.Trim())
                             .ToArray();
        // Assurez-vous qu'il y a suffisamment de parties
        if (messageParts.Length <= 55)
        {
            var Number = int.Parse(messageParts[0].Trim()); // Première partie
            var Name = messageParts[1].Trim(); // Deuxième partie
            var Type1 = messageParts[2].Trim();
            var Type2 = messageParts[3].Trim();
            var Ability_1 = abilities.Length >= 1 ? abilities[0] : " ";
            var Ability_2 = abilities.Length >= 2 ? abilities[1] : " ";
            var Ability_3 = abilities.Length >= 3 ? abilities[2] : " ";
            var HP = int.Parse(messageParts[5]); // Troisième partie
            var Atk = int.Parse(messageParts[6]); // Quatrième partie
            var Def = int.Parse(messageParts[7]);
            var Spa = int.Parse(messageParts[8]);
            var Spd = int.Parse(messageParts[9]);
            var Spe = int.Parse(messageParts[10]);
            var Mean = messageParts[12];
            var StandardDeviation = messageParts[13];
            var Generation = messageParts[14];
            var ExperienceType = messageParts[15];
            var ExperienceToLevel100 = messageParts[16];
            var FinalEvolution = messageParts[17];
            var CatchRate = messageParts[18];
            var Legendary = messageParts[19];
            var MegaEvolution = messageParts[20];
            var AlolanForm = messageParts[21];
            var GalarianForm = messageParts[22];
            var AgainstNormal = double.Parse(messageParts[23]);
            var AgainstFire = double.Parse(messageParts[24]);
            var AgainstWater = double.Parse(messageParts[25]);
            var AgainstElectric = double.Parse(messageParts[26]);
            var AgainstGrass = double.Parse(messageParts[27]);
            var AgainstIce = double.Parse(messageParts[28]);
            var AgainstFighting = double.Parse(messageParts[29]);
            var AgainstPoison = double.Parse(messageParts[30]);
            var AgainstGround = double.Parse(messageParts[31]);
            var AgainstFlying = double.Parse(messageParts[32]);
            var AgainstPsychic = double.Parse(messageParts[33]);
            var AgainstBug = double.Parse(messageParts[34]);
            var AgainstRock = double.Parse(messageParts[35]);
            var AgainstGhost = double.Parse(messageParts[36]);
            var AgainstDragon = double.Parse(messageParts[37]);
            var AgainstDark = double.Parse(messageParts[38]);
            var AgainstSteel = double.Parse(messageParts[39]);
            var AgainstFairy = double.Parse(messageParts[40]);
            var Height = messageParts[41];
            var Weight = messageParts[42];
            var BMI = messageParts[43];

            // Créer une nouvelle connexion à la base de données SQLite
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Créer une nouvelle commande SQL pour insérer les données
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // Définir la commande SQL d'insertion avec des paramètres
                    command.CommandText = @"
                        INSERT INTO PokemonData2 (ID, Number, Name, Type1, Type2, Ability_1, Ability_2, Ability_3)
                        VALUES (@ID, @Number, @Name, @Type1, @Type2, @Ability_1, @Ability_2, @Ability_3);

                        INSERT INTO PokemonStats2 (ID, HP, Att, Def, Spa, Spd, Spe)
                        VALUES (@ID, @HP, @Att, @Def, @Spa, @Spd, @Spe);

                        INSERT INTO PokemonWeakn2 (ID, AgainstNormal, AgainstFire, AgainstWater, AgainstElectric, AgainstGrass, AgainstIce, AgainstFighting, AgainstPoison, AgainstGround, AgainstFlying, AgainstPsychic, AgainstBug, AgainstRock, AgainstGhost, AgainstDragon, AgainstDark, AgainstSteel, AgainstFairy)
                        VALUES (@ID, @AgainstNormal, @AgainstFire, @AgainstWater, @AgainstElectric, @AgainstGrass, @AgainstIce, @AgainstFighting, @AgainstPoison, @AgainstGround, @AgainstFlying, @AgainstPsychic, @AgainstBug, @AgainstRock, @AgainstGhost, @AgainstDragon, @AgainstDark, @AgainstSteel, @AgainstFairy);

                        INSERT INTO PokemonMisc3 (ID, Mean, StandardDeviation, Generation, ExperienceType, ExperienceToLevel100, FinalEvolution, CatchRate, Legendary, MegaEvolution, AlolanForm, GalarianForm, Height, Weight, BMI)
                        VALUES (@ID, @Mean, @StandardDeviation, @Generation, @ExperienceType, @ExperienceToLevel100, @FinalEvolution, @CatchRate, @Legendary, @MegaEvolution, @AlolanForm, @GalarianForm, @Height, @Weight, @BMI);";

                    // Ajouter les paramètres
                    command.Parameters.AddWithValue("@ID", context.MessageId);
                    command.Parameters.AddWithValue("@Number", Number);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Type1", Type1);
                    command.Parameters.AddWithValue("@Type2", Type2);
                    command.Parameters.AddWithValue("@Ability_1", Ability_1);
                    command.Parameters.AddWithValue("@Ability_2", Ability_2);
                    command.Parameters.AddWithValue("@Ability_3", Ability_3);
                    command.Parameters.AddWithValue("@HP", HP);
                    command.Parameters.AddWithValue("@Att", Atk);
                    command.Parameters.AddWithValue("@Def", Def);
                    command.Parameters.AddWithValue("@Spa", Spa);
                    command.Parameters.AddWithValue("@Spd", Spd);
                    command.Parameters.AddWithValue("@Spe", Spe);
                    command.Parameters.AddWithValue("@Mean", Mean);
                    command.Parameters.AddWithValue("@StandardDeviation", StandardDeviation);
                    command.Parameters.AddWithValue("@Generation", Generation);
                    command.Parameters.AddWithValue("@ExperienceType", ExperienceType);
                    command.Parameters.AddWithValue("@ExperienceToLevel100", ExperienceToLevel100);
                    command.Parameters.AddWithValue("@FinalEvolution", FinalEvolution);
                    command.Parameters.AddWithValue("@CatchRate", CatchRate);
                    command.Parameters.AddWithValue("@Legendary", Legendary);
                    command.Parameters.AddWithValue("@MegaEvolution", MegaEvolution);
                    command.Parameters.AddWithValue("@AlolanForm", AlolanForm);
                    command.Parameters.AddWithValue("@GalarianForm", GalarianForm);
                    command.Parameters.AddWithValue("@AgainstNormal", AgainstNormal);
                    command.Parameters.AddWithValue("@AgainstFire", AgainstFire);
                    command.Parameters.AddWithValue("@AgainstWater", AgainstWater);
                    command.Parameters.AddWithValue("@AgainstElectric", AgainstElectric);
                    command.Parameters.AddWithValue("@AgainstGrass", AgainstGrass);
                    command.Parameters.AddWithValue("@AgainstIce", AgainstIce);
                    command.Parameters.AddWithValue("@AgainstFighting", AgainstFighting);
                    command.Parameters.AddWithValue("@AgainstPoison", AgainstPoison);
                    command.Parameters.AddWithValue("@AgainstGround", AgainstGround);
                    command.Parameters.AddWithValue("@AgainstFlying", AgainstFlying);
                    command.Parameters.AddWithValue("@AgainstPsychic", AgainstPsychic);
                    command.Parameters.AddWithValue("@AgainstBug", AgainstBug);
                    command.Parameters.AddWithValue("@AgainstRock", AgainstRock);
                    command.Parameters.AddWithValue("@AgainstGhost", AgainstGhost);
                    command.Parameters.AddWithValue("@AgainstDragon", AgainstDragon);
                    command.Parameters.AddWithValue("@AgainstDark", AgainstDark);
                    command.Parameters.AddWithValue("@AgainstSteel", AgainstSteel);
                    command.Parameters.AddWithValue("@AgainstFairy", AgainstFairy);
                    command.Parameters.AddWithValue("@Height", Height);
                    command.Parameters.AddWithValue("@Weight", Weight);
                    command.Parameters.AddWithValue("@BMI", BMI);

                    // Exécuter la commande SQL
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Data has been successfully inserted into the database, Context: {context.MessageHeaders}, {context}");

            return Task.CompletedTask;
        }
        else
        {
            Console.WriteLine("Invalid message format. Message must contain two parts separated by a comma.");
            return Task.CompletedTask;
        }
    }
}

public class MyMessage : IMessage
{
    public string Data { get; set; } = string.Empty;
}
