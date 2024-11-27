using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using RealMonsterData.Pocos;

namespace Pocos
{
    public partial class integratedTwinController
    {
        public MonsterData.Pocos.Monster Monster { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster OnlineToPlain_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster PlainToOnline_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster OnlineToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ShadowToOnlineAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectOnlineToPlain_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectPlainToOnline_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectOnlineToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectShadowToOnlineAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ShadowToPlainAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster PlainToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectShadowToPlainAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public MonsterData.Pocos.Monster ITwinObjectPlainToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Pocos.Monster();
        public global::Pocos.Pokus Pokus { get; set; } = new global::Pocos.Pokus();
        public RealMonsterData.Pocos.RealMonster RealMonster { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster OnlineToShadow_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ShadowToOnline_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster OnlineToPlain_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster PlainToOnline_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ITwinObjectOnlineToShadow_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ITwinObjectShadowToOnline_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ITwinObjectOnlineToPlain_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ITwinObjectPlainToOnline_should_copy { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public global::Pocos.all_primitives p_online_shadow { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.all_primitives p_shadow_online { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.all_primitives p_online_plain { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.all_primitives p_plain_online { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.all_primitives p_shadow_plain { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.all_primitives p_plain_shadow { get; set; } = new global::Pocos.all_primitives();
        public RealMonsterData.Pocos.RealMonster StartPolling_should_update_cyclic_property { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster StartPolling_ConcurentOverload { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public RealMonsterData.Pocos.RealMonster ChangeDetections { get; set; } = new RealMonsterData.Pocos.RealMonster();
        public GH_ISSUE_183.Pocos.GH_ISSUE_183_1 GH_ISSUE_183 { get; set; } = new GH_ISSUE_183.Pocos.GH_ISSUE_183_1();
    }
}

namespace Pocos
{
    public partial class Pokus : AXSharp.Connector.IPlain
    {
        public Pokus()
        {
        }
    }
}

namespace Pocos
{
    public partial class Nested : AXSharp.Connector.IPlain
    {
        public Nested()
        {
        }
    }
}