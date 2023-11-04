# Teamfight Tactics Tracker

Teamfight Tactics Tracker è una semplice applicazione da riga di comando scritta in C# che ti consente di tenere traccia delle tue partite a Teamfight Tactics (TFT). Con questa app, puoi salvare in un file CSV informazioni sui personaggi, gli oggetti, i livelli, il piazzamento, le abilità scelte e i tratti usati nelle tue partite a TFT.

<img width="997" alt="Screenshot 2023-11-04 alle 19 49 03" src="https://github.com/MoxPy/teamfight-tactics-tracker/assets/80635030/b6410cc4-e701-4c73-9f9b-862484d453a1">
<img width="618" alt="Screenshot 2023-11-04 alle 19 51 03" src="https://github.com/MoxPy/teamfight-tactics-tracker/assets/80635030/adcea685-b369-46bb-8d32-82fef8cd0f2f">

### Caratteristiche
Registra informazioni sulle tue partite a TFT, inclusi personaggi, oggetti, livelli, piazzamento, abilità e tratti.
Archivia i dati delle tue partite in un file CSV per facilitare l'analisi e la revisione.

### Come Utilizzare

### Installazione
Assicurati di avere .NET Core installato sul tuo sistema.
Clona o scarica questa repository sulla tua macchina locale.

### Esecuzione dell'Applicazione
Apri il tuo terminale o prompt dei comandi.
Naviga nella directory del progetto.
Esegui l'applicazione inserendo il seguente comando:

        dotnet run

### Inserimento dei Dati
Segui le istruzioni dell'applicazione per inserire le informazioni sulle tue partite.
Puoi inserire dettagli sui personaggi, gli oggetti, i livelli, il piazzamento, le abilità e i tratti mentre giochi le tue partite.

### Esportazione dei Dati
Una volta inseriti tutti i dati ti verrà chiesto se vuoi salvare le informazioni in un file CSV strutturato appositamente.

### Formato del CSV
Il file CSV contiene le seguenti colonne:
 - _Personaggi_: I personaggi utilizzati nella partita.
 - _Livello Personaggi_: Il livello di stelle raggiunto da un determinato personaggio.
 - _Oggetti_: Oggetti associati a ciascun personaggio.
 - _Leggenda_: La leggenda selezionata.
 - _Piazzamento_: Il tuo piazzamento nella partita.
 - _Abilità_: Abilità principale della partita (il campo in cui si gioca) e quelle scelte durante.
 - _Tratti_: Tratti associati alla tua squadra e il loro livello.

### Licenza
Questa applicazione è open source ed è rilasciata con la Licenza Mozilla Public License 2.0.

Se riscontri problemi o hai suggerimenti per miglioramenti, crea una segnalazione nell'apposita sezione della repository GitHub.
