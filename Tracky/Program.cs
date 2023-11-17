
namespace Tracky
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto nell'applicazione per tracciare le tue partite sul TFT!!");
            
            // Assegnazione variabili
            bool phaseOne = false;
            bool phaseTwo = false;
            int limitNames = 10;
            int limitAuguments = 3;
            int limitItems = 3;
            List<Champion> champions = new List<Champion>();
            List<string> auguments = new List<string>();
            List<Trait> traits = new List<Trait>();
            //
            
            Console.Write("Quanto ti sei classificato? ");
            string placement = Console.ReadLine();
            
            Console.Write("Inserisci la tua Leggenda: ");
            string legend = Console.ReadLine();
            
            Console.Write("Inserisci l'augument principale della partita: ");
            string mainAugument = Console.ReadLine();
            
            while (champions.Count < limitNames)
            {
                Console.Write("Inserisci un personaggio, solo il nome (o premi INVIO per terminare): ");  
                
                Champion pg = new Champion();

                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    phaseOne = true;
                    break;
                }
                pg.Name = name;
                Console.Write($"Livello di {name}: "); 
                string championLvl = Console.ReadLine();
                int level;
                if (int.TryParse(championLvl, out level))
                {
                    pg.Level = level;
                    Console.WriteLine($"Hai aggiunto {pg.Name} livello: {pg.Level}");
                    champions.Add(pg);
                }
                else
                {
                    Console.WriteLine("Input non valido. Assicurati di inserire un numero intero.");
                }
                
                
            }

            
            while (phaseOne)
            {
                Console.WriteLine("FASE DUE");
                Console.WriteLine("|||||||||||||\nPersonaggi inseriti:");
                for (int i = 0; i < champions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {champions[i].Name}");
                }

                Console.Write("Seleziona un numero per assegnare un oggetto ad un personaggio (o premi INVIO per andare allo step successivo): ");
                string inputScelta = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputScelta))
                {
                    phaseTwo = true;
                    break;
                }

                if (int.TryParse(inputScelta, out int scelta) && scelta >= 1 && scelta <= champions.Count)
                {
                    var champ = champions[scelta - 1];
                    List<string> items = new List<string>();

                    while (true)
                    {
                        if (items.Count == 3)
                        {
                            phaseTwo = true;
                            break;
                        }
                        Console.Write($"Inserisci un oggetto per {champ.Name} (o premi INVIO per terminare l'aggiunta di oggetti a {champ.Name}): ");
                        string item = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            phaseTwo = true;
                            break;
                        }
                        items.Add(item);
                    }

                    if (items.Count > 0)
                    {
                        Console.WriteLine($"Hai assegnato i seguenti oggetti al personaggio {champ.Name}:");

                        foreach (var i in items)
                        {
                            Console.WriteLine(i);
                        }

                        champ.Items = new List<string>(items);
                    }
                }

                
            }

            while (phaseTwo)
            {
                Console.WriteLine("FASE TRE");
                Console.WriteLine("|||||||||||||\nAggiungi i tratti di questa partita");
                while (true)
                { 
                    Console.WriteLine("Nome tratto (o premi INVIO per andare allo step successivo):");
                    string traitName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(traitName))
                    {
                        break;
                    }
                    Console.WriteLine("Livello tratto");
                    string traitLevelStr = Console.ReadLine();
                    Trait trait = new Trait();

                    trait.Name = traitName;
                    if (int.TryParse(traitLevelStr, out int traitLevel))
                    {
                        trait.Level = traitLevel;
                    }

                    traits.Add(trait);
                }
                Console.WriteLine("Vuoi continuare ad aggiungere tratti? (s/n)");
                string continueAdding = Console.ReadLine();
                if (continueAdding.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            
            while (auguments.Count < limitAuguments)
            {
                Console.WriteLine("Aggiungi gli augument che hai selezionato (o premi INVIO per uscire):");
                string aug = Console.ReadLine();
                auguments.Add(aug);
                if (string.IsNullOrWhiteSpace(aug))
                {
                    break;
                }
            }
            
            Console.WriteLine("\n||||||||||||||| \nRiassunto finale\n|||||||||||||||\n");
            Console.WriteLine("\n||\nPIAZZAMENTO\n||\n");
            Console.WriteLine($"Sei arrivato: {placement}");
            Console.WriteLine("\n||\nLEGGENDA\n||\n");
            Console.WriteLine($"La tua leggenda: {legend}");
            Console.WriteLine("\n||\nAUGUMENT\n||\n");
            Console.WriteLine($"Augument principale: {mainAugument}");
            Console.WriteLine("I tuoi augument per questa partita:");
            foreach (var augument in auguments)
            {
                Console.WriteLine($"{augument}");
            }
            Console.WriteLine("\n||\nTRATTI\n||\n");
            Console.WriteLine("I tuoi tratti attivi per questa partita:");
            foreach (var t in traits)
            {
                Console.WriteLine($"Nome: {t.Name}\nLivello: {t.Level}");
            }
            Console.WriteLine("\n||\nPERSONAGGI\n||\n");
            foreach (var champion in champions)
            {
                if (champion.Items != null && champion.Items.Count > 0)
                {
                    Console.WriteLine($"\n{champion.Name.ToUpper()} è livello {champion.Level} con:");
                    if (champion.Items.Count > 0)
                    { 
                        foreach (var item in champion.Items)
                        {
                            Console.WriteLine(item);
                        }
                    
                    }
                }
                else
                {
                    Console.WriteLine($"\n{champion.Name.ToUpper()} è livello {champion.Level} senza oggetti");
                }
                
            }

            while (true)
            {
                Console.WriteLine("Vuoi salvare la partita? s/n");
                string saveMatch = Console.ReadLine();
                if (saveMatch.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                } else if (saveMatch.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    DataManagement dataManager = new DataManagement();
                    bool saveIsOk = false;
                    while (true)
                    {
                        async Task SaveGameTask()
                        {
                            Console.WriteLine("Che tipo di salvataggio vuoi utilizzare (inserisci il numero corrispondente alla tua scelta)?\n1. CSV Locale\n2. Airtable");
                            string saveType = Console.ReadLine();
                            // Local
                            if (saveType.Equals("1", StringComparison.OrdinalIgnoreCase))
                            {
                                dataManager.LocalSave(champions, auguments, traits, placement, legend, mainAugument);
                                saveIsOk = true;
                                
                            } 
                            // Airtable 
                            else if (saveType.Equals("2", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Airtable integration in arrivo");
                                await dataManager.AirtableSave(champions, auguments, traits, placement, legend,
                                    mainAugument);
                                saveIsOk = true;
                            } else
                            {
                                Console.WriteLine("Puoi scegliere solo 1 o 2. Riprova");
                            }
                        }
                        SaveGameTask().Wait();
                        if (saveIsOk)
                        {
                            break;
                        }
                    }
                    
                }
                break;
            }

        }
    }
}
