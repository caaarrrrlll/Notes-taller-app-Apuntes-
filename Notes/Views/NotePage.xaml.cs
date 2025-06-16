namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{

    public NotePage()
    {
        InitializeComponent();
        BindingContext = new Models.Note(); // Para nuevas notas
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // Si es una nota nueva, crea un archivo nuevo
            if (string.IsNullOrEmpty(note.Filename))
            {
                note.Filename = Path.Combine(
                    FileSystem.AppDataDirectory,
                    $"{Path.GetRandomFileName()}.notes.txt");
            }

            File.WriteAllText(note.Filename, TextEditor.Text);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    //arreglado con chat gpt ---------------------------------------
    private void LoadNote(string filename)
    {
        if (File.Exists(filename))
        {
            var noteText = File.ReadAllText(filename);
            var note = new Models.Note
            {
                Filename = filename,
                Text = noteText
            };
            BindingContext = note;
            TextEditor.Text = noteText; // Esto llena el Editor con el texto de la nota
        }
    }

    public string ItemId
    {
        set { LoadNote(value); }
    }

    // Fixed CS0825 error: Moved the declaration of 'filename' inside a method.
    private void CreateNewNote()
    {
        string filename = Path.Combine(FileSystem.AppDataDirectory, $"{Path.GetRandomFileName()}.notes.txt");
        File.WriteAllText(filename, TextEditor.Text);
    }

    //--------------------------------------------------
}