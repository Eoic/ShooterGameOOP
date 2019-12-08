namespace Server.Game.Commands
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }
}
