namespace Snake_box
{
    public class HealBonus: BaseBonus
    {
        public HealBonus(HealBonusData BonusData) : base(BonusData)
        {
        }

        public override void Use()
        {
            Services.Instance.LevelService.CharacterBehaviour.Heal();
            base.Use();
        }
    }
}
