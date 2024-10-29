namespace Asteroids.Utilities
{
    public interface ISaveManager
    {
        void SetData<T>(string key, T value);
        T GetData<T>(string key, T defaultValue = default);
        void Save();
        void Load();
    }
}