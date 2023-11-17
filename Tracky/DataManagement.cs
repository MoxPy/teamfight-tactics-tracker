using AirtableApiClient;

namespace Tracky;

public class DataManagement
{
    public void LocalSave(List<Champion> champions, List<string> auguments, List<Trait> traits, string placement,
        string legend, string mainAugument)
    {
        Console.WriteLine("Inserisci il path del file (senza estensioni, es. /Users/utente/Desktop/teamfight)");
        string filePath = Console.ReadLine();
        string suff = ".csv";
        int pIndex = 0;
        using (StreamWriter sw = new StreamWriter(filePath + suff))
        {
            // Dati dei personaggi
            sw.WriteLine(
                "Personaggi,Livello Personaggio,Oggetti Personaggio,Leggenda,Piazzamento,Augument Principale,Auguments,Tratti,Livello Tratti");
            foreach (var champion in champions)
            {
                if (champion.Items != null)
                {
                    string oggetti = string.Join(" - ", champion.Items);
                    if (pIndex == 0)
                    {
                        sw.WriteLine(
                            $"{champion.Name},{champion.Level},{oggetti},{legend},{placement},{mainAugument},{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                    }
                    else if (pIndex == 1)
                    {
                        sw.WriteLine(
                            $"{champion.Name},{champion.Level},{oggetti},,,,{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                    }
                    else if (pIndex == 2)
                    {
                        if (traits.Count > 2)
                        {
                            sw.WriteLine(
                                $"{champion.Name},{champion.Level},{oggetti},,,,{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                        }
                        else
                        {
                            sw.WriteLine($"{champion.Name},{champion.Level},{oggetti},,,,{auguments[pIndex]},,,");
                        }
                    }
                    else
                    {
                        if (pIndex <= traits.Count - 1)
                        {
                            sw.WriteLine(
                                $"{champion.Name},{champion.Level},{oggetti},,,,,{traits[pIndex].Name},{traits[pIndex].Level}");
                        }
                        else
                        {
                            sw.WriteLine($"{champion.Name},{champion.Level},{oggetti},,,,,,");
                        }
                    }
                }
                else
                {
                    if (pIndex == 0)
                    {
                        sw.WriteLine(
                            $"{champion.Name},{champion.Level},,{legend},{placement},{mainAugument},{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                    }
                    else if (pIndex == 1)
                    {
                        sw.WriteLine(
                            $"{champion.Name},{champion.Level},,,,,{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                    }
                    else if (pIndex == 2)
                    {
                        if (traits.Count > 2)
                        {
                            sw.WriteLine(
                                $"{champion.Name},{champion.Level},,,,,{auguments[pIndex]},{traits[pIndex].Name},{traits[pIndex].Level}");
                        }
                        else
                        {
                            sw.WriteLine($"{champion.Name},{champion.Level},,,,,{auguments[pIndex]},,,");
                        }
                    }
                    else
                    {
                        if (pIndex <= traits.Count - 1)
                        {
                            sw.WriteLine(
                                $"{champion.Name},{champion.Level},,,,,,{traits[pIndex].Name},{traits[pIndex].Level}");
                        }
                        else
                        {
                            sw.WriteLine($"{champion.Name},{champion.Level},,,,,,");
                        }
                    }
                }

                pIndex++;
            }

            if (traits.Count > champions.Count)
            {
                if (pIndex >= champions.Count - 1)
                {
                    if (pIndex < traits.Count - 1)
                    {
                        sw.WriteLine($",,,,,,,{traits[pIndex].Name},{traits[pIndex].Level}");
                        pIndex++;
                    }
                }
            }
        }

        Console.WriteLine("File CSV creato con successo.");
    }

    public async Task AirtableSave(List<Champion> champions, List<string> auguments, List<Trait> traits, string placement,
        string legend, string mainAugument)
    {
        
    string offset = null;
    
    Console.WriteLine("Inserisci il tuo base ID (lo trovi nell'url https://airtable.com/baseId/tableId/)");
    string baseId = Console.ReadLine();
    
    Console.WriteLine("Inserisci il tuo table ID (lo trovi nell'url https://airtable.com/baseId/tableId/)");
    string tableId = Console.ReadLine();
    
    Console.WriteLine("Inserisci il tuo access token (devi impostarlo dalla sezione apposita di Airtable)");
    string accessToken = Console.ReadLine();
    
    using (AirtableBase airtableBase = new AirtableBase(accessToken, baseId))
    {
        
        Fields[] fields = new Fields[champions.Count];
        int i = 0;
        foreach (var champion in champions)
        {
            fields[i] = new Fields();
            fields[i].AddField("Personaggio", champion.Name);
            fields[i].AddField("Livello Personaggio", champion.Level);
            if (champion.Items != null && champion.Items.Any())
            { 
                fields[i].AddField("Oggetti Personaggio", string.Join(" - ", champion.Items));
            }
            else
            {
                fields[i].AddField("Oggetti Personaggio", "Nessun oggetto");
            }

            if (i == 0)
            {
                fields[i].AddField("Leggenda", legend);
                fields[i].AddField("Piazzamento", placement);
                fields[i].AddField("Augument Principale", mainAugument);
            }

            if (auguments.Count != 0)
            {
                if (i <= auguments.Count - 1)
                {
                    fields[i].AddField("Auguments", auguments[i]);
                }
            }

            if (traits.Count != 0)
            {
                if (i <= traits.Count - 1)
                {
                    fields[i].AddField("Tratti", traits[i].Name);
                    fields[i].AddField("Livello Tratti", traits[i].Level);
                }
            }
                
            i++;
        }
        
        
        Task<AirtableCreateUpdateReplaceMultipleRecordsResponse> task = airtableBase.CreateMultipleRecords(tableId, fields, true);
        var response = await task;
       
            
        if (!response.Success)
        {
            string errorMessage = null;
            if (response.AirtableApiError is AirtableApiException)
            {
                errorMessage = response.AirtableApiError.ErrorMessage;
                if (response.AirtableApiError is AirtableInvalidRequestException)
                {
                    errorMessage += "\nDetailed error message: ";
                    errorMessage += response.AirtableApiError.DetailedErrorMessage;
                }
            }
            else
            {
                errorMessage = "Unknown error";
            }
            // Report errorMessage
        }
        else
        {
            AirtableRecord[] records = response.Records;
            // Console.WriteLine(records);
        }
        
    }

    }
}

