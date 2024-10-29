namespace Asteroids.Borders
{
    public interface IScreenBoundsRecycler
    {
        void RegisterWrapRecycler(IWrapRecycler wrapRecycler);
        void UnregisterWrapRecycler(IWrapRecycler wrapRecycler);
    }
}