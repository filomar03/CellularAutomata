namespace CellularAutomata
{
    internal interface IInteractable
    {
        //this class should allow the cursor to be interactable (by reading keys on another thread)
        //then the cursor should be moved or a pseudo cursor should be printed in the formatted string
        //this could also allow to react to other keypresses
        void MoveCursor();
    }
}
