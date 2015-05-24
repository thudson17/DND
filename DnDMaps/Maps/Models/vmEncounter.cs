using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maps.Models
{
    public class vmEncounter
    {
        public vmEncounter()
        {
            this.PlayerTeam = new List<Character>();
            this.PossibleCharacters = new List<Character>();
            this.MonsterTeam = new List<Monster>();
            this.PossibleMonsters = new List<Monster>();
        }

        [DisplayName("Encounter Identifier")]
        public int Encounter_ID { get; set; }

        [Required]
        public string Name { get; set; }

        //public List<> TurnOrder { get; set; }

        public List<Character> PossibleCharacters { get; set; }
        public List<Character> PlayerTeam { get; set; }

        public List<Monster> PossibleMonsters { get; set; }
        public List<Monster> MonsterTeam { get; set; }

        public MultiSelectList GetCharacterListItems()
        {
            List<Character> characters = this.PossibleCharacters;
            IEnumerable<SelectListItem> characterList = new List<SelectListItem>();

            if (characters != null)
            {
                characterList = characters.OrderBy(character => character.Name)
                    .Select(character => new SelectListItem { Selected = this.PlayerTeam.Contains(character), Text = character.Name, Value = character.Character_ID.ToString() });
            }
            return new MultiSelectList(characterList, "Value", "Text");
        }

        public MultiSelectList GetMonsterListItems()
        {
            List<Monster> monsters = this.PossibleMonsters;
            IEnumerable<SelectListItem> characterList = new List<SelectListItem>();

            if (monsters != null)
            {
                characterList = monsters.OrderBy(monster => monster.Name)
                    .Select(monster => new SelectListItem { Selected = this.MonsterTeam.Contains(monster), Text = monster.Name, Value = monster.Monster_ID.ToString() });
            }

            return new MultiSelectList(characterList, "Value", "Text");
        }
    }
}