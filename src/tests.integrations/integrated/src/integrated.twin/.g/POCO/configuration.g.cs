using System;

namespace Pocos
{
    using RealMonsterData;

    public partial class integratedTwinController
    {
        public MonsterData.Monster Monster { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster OnlineToPlain_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster PlainToOnline_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster OnlineToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ShadowToOnlineAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectOnlineToPlain_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectPlainToOnline_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectOnlineToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectShadowToOnlineAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ShadowToPlainAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster PlainToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectShadowToPlainAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ITwinObjectPlainToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public Pokus Pokus { get; set; } = new Pokus();
        public RealMonsterData.RealMonster RealMonster { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster OnlineToShadow_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ShadowToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster OnlineToPlain_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster PlainToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ITwinObjectOnlineToShadow_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ITwinObjectShadowToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ITwinObjectOnlineToPlain_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ITwinObjectPlainToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public all_primitives p_online_shadow { get; set; } = new all_primitives();
        public all_primitives p_shadow_online { get; set; } = new all_primitives();
        public all_primitives p_online_plain { get; set; } = new all_primitives();
        public all_primitives p_plain_online { get; set; } = new all_primitives();
        public all_primitives p_shadow_plain { get; set; } = new all_primitives();
        public all_primitives p_plain_shadow { get; set; } = new all_primitives();
        public RealMonsterData.RealMonster StartPolling_should_update_cyclic_property { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster StartPolling_ConcurentOverload { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ChangeDetections { get; set; } = new RealMonsterData.RealMonster();
        public GH_ISSUE_183.GH_ISSUE_183_1 GH_ISSUE_183 { get; set; } = new GH_ISSUE_183.GH_ISSUE_183_1();
    }

    public partial class Pokus : AXSharp.Connector.IPlain
    {
    }

    public partial class Nested : AXSharp.Connector.IPlain
    {
    }
}