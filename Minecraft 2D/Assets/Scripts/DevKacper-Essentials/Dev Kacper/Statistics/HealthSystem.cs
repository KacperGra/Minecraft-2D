namespace DevKacper.Mechanic
{
    public class HealthSystem
    {
        public enum State
        {
            Alive,
            Dead
        }

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        private State state;

        public HealthSystem(int healthValue)
        {
            MaxHealth = healthValue;
            Health = healthValue;
            state = State.Alive;
        }

        public void TakeDamage(int value)
        {
            Health -= value;
            if (Health <= 0)
            {
                state = State.Dead;
            }
        }

        public void Heal(int value)
        {
            Health += value;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            if (Health > 0)
            {
                state = State.Alive;
            }
        }

        public bool IsAlive()
        {
            return state == State.Alive;
        }

        public float GetHealthRatio()
        {
            return (float)Health / MaxHealth;
        }

        public void ChangeMaxHealth(int health, bool SetHealthEqualToMax = false)
        {
            MaxHealth = health;
            if (SetHealthEqualToMax)
            {
                Health = MaxHealth;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Health.ToString(), MaxHealth.ToString());
        }
    }
}