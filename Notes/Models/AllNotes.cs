using System.Collections.ObjectModel;

namespace Notes.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();
        var files = Directory.EnumerateFiles(FileSystem.AppDataDirectory, "*.notes.txt");

        foreach (var filename in files)
        {
            var text = File.ReadAllText(filename);
            Notes.Add(new Note
            {
                Filename = filename,
                Text = text
            });
        }
    }
}