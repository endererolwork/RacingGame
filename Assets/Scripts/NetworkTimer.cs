namespace Race
{
    public class NetworkTimer
    {
        private float timer;
        public float MinTimeBetweenTics { get; }

        public int CurrentTick { get;  set; }

        public NetworkTimer(float serverTickRate)
        {
            MinTimeBetweenTics = 1f / serverTickRate;
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
        }

        public bool ShouldTick()
        {
            if (timer >= MinTimeBetweenTics)
            {
                timer -= MinTimeBetweenTics;
                CurrentTick++;
                return true;
            }

            return false;
        }
        
       
    }
}