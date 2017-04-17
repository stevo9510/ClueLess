public class MoveModel
{
    public int MoveID { get; set; }

    public int? LocationID { get; set; }

    public override string ToString()
    {
        
        // TODO: This needs to generate a friendly name for the Move. 
        return base.ToString();
    }

}
