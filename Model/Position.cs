public struct Position
{
    public int Index { get; private set; }
    public int Line { get; private set; }
    public int Column { get; private set; }
    public string FileName { get; private set; }
    public string FileText { get; private set; }

    public Position(int index, int line, int column, string fileName, string fileText)
    {
        Index = index;
        Column = column;
        Line = line;
        FileName = fileName;
        FileText = fileText;
    }

    public void MoveNext(char? сurrentSymbol = null)
    {
        Index++;
        Column++;

        if(сurrentSymbol == '\n')
        {
            Line++;
            Column = 0;
        }
    }
    
    public Position Copy() => new Position(Index, Line, Column, FileName, FileText);
}