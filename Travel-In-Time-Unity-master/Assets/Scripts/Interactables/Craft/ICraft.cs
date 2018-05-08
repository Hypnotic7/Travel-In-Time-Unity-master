namespace Assets.Scripts.Interactables.Craft
{
    public interface ICraft<T>
    {
        bool CheckForIngrediants();
        void Reward();
    }
}
