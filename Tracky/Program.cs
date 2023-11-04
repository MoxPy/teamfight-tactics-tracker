namespace Tracky
{
    class Personaggio
    {
        public string Nome { get; set; }
        public List<string> Oggetti { get; set; }
        public int Livello { get; set; }
    }

    class Tratto
    {
        public string Nome { get; set; }
        public int Livello { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto nell'applicazione per tracciare le tue partite sul TFT!!");
            
            // Assegnazione variabili
            bool phaseOne = false;
            bool phaseTwo = false;
            int limiteNomi = 10;
            int limiteAuguments = 3;
            int limiteItems = 3;
            List<Personaggio> personaggi = new List<Personaggio>();
            List<string> auguments = new List<string>();
            List<Tratto> tratti = new List<Tratto>();
            //
            
            Console.Write("Quanto ti sei classificato? ");
            string piazzamento = Console.ReadLine();
            
            Console.Write("Inserisci la tua Leggenda: ");
            string legend = Console.ReadLine();
            
            Console.Write("Inserisci l'augument principale della partita: ");
            string mainAugument = Console.ReadLine();
            
            while (personaggi.Count < limiteNomi)
            {
                Console.Write("Inserisci un personaggio, solo il nome (o premi INVIO per terminare): ");  
                
                Personaggio pg = new Personaggio();

                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    phaseOne = true;
                    break;
                }
                pg.Nome = name;
                Console.Write($"Livello di {name}: "); 
                string lvlPersonaggio = Console.ReadLine();
                int livello;
                if (int.TryParse(lvlPersonaggio, out livello))
                {
                    pg.Livello = livello;
                    Console.WriteLine($"Hai aggiunto {pg.Nome} livello: {pg.Livello}");
                    personaggi.Add(pg);
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
                for (int i = 0; i < personaggi.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {personaggi[i].Nome}");
                }

                Console.Write("Seleziona un numero per assegnare un oggetto ad un personaggio (o premi INVIO per andare allo step successivo): ");
                string inputScelta = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputScelta))
                {
                    phaseTwo = true;
                    break;
                }

                if (int.TryParse(inputScelta, out int scelta) && scelta >= 1 && scelta <= personaggi.Count)
                {
                    var personaggio = personaggi[scelta - 1];
                    List<string> items = new List<string>();

                    while (true)
                    {
                        if (items.Count == 3)
                        {
                            phaseTwo = true;
                            break;
                        }
                        Console.Write($"Inserisci un oggetto per {personaggio.Nome} (o premi INVIO per terminare l'aggiunta di oggetti a {personaggio.Nome}): ");
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
                        Console.WriteLine($"Hai assegnato i seguenti oggetti al personaggio {personaggio.Nome}:");

                        foreach (var i in items)
                        {
                            Console.WriteLine(i);
                        }

                        personaggio.Oggetti = new List<string>(items);
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
                    Tratto trait = new Tratto();

                    trait.Nome = traitName;
                    if (int.TryParse(traitLevelStr, out int traitLevel))
                    {
                        trait.Livello = traitLevel;
                    }

                    tratti.Add(trait);
                }
                Console.WriteLine("Vuoi continuare ad aggiungere tratti? (s/n)");
                string continueAdding = Console.ReadLine();
                if (continueAdding.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            
            while (auguments.Count < limiteAuguments)
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
            Console.WriteLine($"Sei arrivato: {piazzamento}");
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
            foreach (var t in tratti)
            {
                Console.WriteLine($"Nome: {t.Nome}\nLivello: {t.Livello}");
            }
            Console.WriteLine("\n||\nPERSONAGGI\n||\n");
            foreach (var personaggio in personaggi)
            {
                if (personaggio.Oggetti != null && personaggio.Oggetti.Count > 0)
                {
                    Console.WriteLine($"\n{personaggio.Nome.ToUpper()} è livello {personaggio.Livello} con:");
                    if (personaggio.Oggetti.Count > 0)
                    { 
                        foreach (var oggetto in personaggio.Oggetti)
                        {
                            Console.WriteLine(oggetto);
                        }
                    
                    }
                }
                else
                {
                    Console.WriteLine($"\n{personaggio.Nome.ToUpper()} è livello {personaggio.Livello} senza oggetti");
                }
                
            }

            while (true)
            {
                Console.WriteLine("Vuoi salvare la partita? s/n");
                string salvaPartita = Console.ReadLine();
                if (salvaPartita.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                } else if (salvaPartita.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Inserisci il path del file (senza estensioni, es. /Users/utente/Desktop/teamfight)");
                    string filePath = Console.ReadLine();
                    string suff = ".csv";
                    int pIndex = 0;
                    using (StreamWriter sw = new StreamWriter(filePath + suff))
                    {
                        // Dati dei personaggi
                        sw.WriteLine("Personaggi,Livello Personaggio,Oggetti Personaggio,Leggenda,Piazzamento,Augument Principale,Auguments,Tratti,Livello Tratti");
                        foreach (var personaggio in personaggi)
                        {
                            if (personaggio.Oggetti != null)
                            {
                                string oggetti = string.Join(" - ", personaggio.Oggetti);
                                if (pIndex == 0)
                                {
                                    sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},{legend},{piazzamento},{mainAugument},{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                }
                                else if (pIndex == 1)
                                {
                                    sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},,,,{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                } else if (pIndex == 2)
                                {
                                    if (tratti.Count > 2)
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},,,,{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                    }
                                    else
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},,,,{auguments[pIndex]},,,");

                                    }
                                }
                                else
                                {
                                    if (pIndex <= tratti.Count - 1)
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},,,,,{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                    }
                                    else
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},{oggetti},,,,,,");
                                    }
                                }
                            }
                            else
                            {
                                if (pIndex == 0)
                                {
                                    sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,{legend},{piazzamento},{mainAugument},{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                }
                                else if (pIndex == 1)
                                {
                                    sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,,,,{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                } else if (pIndex == 2)
                                {
                                    if (tratti.Count > 2)
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,,,,{auguments[pIndex]},{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                    }
                                    else
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,,,,{auguments[pIndex]},,,");
                                    }
                                    
                                } else 
                                {
                                    if (pIndex <= tratti.Count - 1)
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,,,,,{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                    }
                                    else
                                    {
                                        sw.WriteLine($"{personaggio.Nome},{personaggio.Livello},,,,,,");
                                    }
                                }
                            }

                            pIndex++;
                        }

                        if (tratti.Count > personaggi.Count)
                        {
                            if (pIndex >= personaggi.Count - 1)
                            {
                                if (pIndex < tratti.Count - 1)
                                {
                                    sw.WriteLine($",,,,,,,{tratti[pIndex].Nome},{tratti[pIndex].Livello}");
                                    pIndex++;
                                }
                            }
                        }
                        
                    }

                    Console.WriteLine("File CSV creato con successo.");
                    break;
                }
                
            }

        }
    }
}
