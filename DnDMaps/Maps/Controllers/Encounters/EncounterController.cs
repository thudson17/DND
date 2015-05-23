using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maps.Models;
using System.Configuration;
using System.IO;
using AutoMapper;

namespace Maps.Controllers
{
    public class EncounterController : Controller
    {
        private Entities db = new Entities(); //database context class, should be cleaned up on de-struct

        //Root Encounter Page, Show existing list of all Encounters
        public ActionResult Index()
        {
            Mapper.CreateMap<Encounter, vmEncounter>(); //auto map entity model to view model

            var model = db.Encounters.ToList();

            return View(Mapper.Map(model, new List<vmEncounter>()));
        }

        //Create a new Encounter
        public ActionResult Create()
        {
            List<Character> possibleCharacters = db.Characters.ToList();
            List<Monster> possibleMonsters = db.Monsters.ToList();

            vmEncounter model = new vmEncounter();

            model.PossibleCharacters = possibleCharacters;
            model.PossibleMonsters = possibleMonsters;

            return View(model);
        }

        //
        // POST: /Encounter/Create
        [HttpPost]
        public ActionResult Create(vmEncounter Encounter, int[] playerIds, int[] monsterIds)
        {
            List<Character> possibleCharacters = db.Characters.ToList();
            List<Monster> possibleMonsters = db.Monsters.ToList();

            if (ModelState.IsValid)
            {
                Mapper.CreateMap<vmEncounter, Encounter>(); //auto map view model to a fresh entity model...
                Encounter newEncounter = new Encounter();
                Mapper.Map(Encounter, newEncounter);

                db.Encounters.Add(newEncounter);
                db.SaveChanges();

                List<Encounter_Character> playerTeam = new List<Encounter_Character>();
                foreach(int playerId in playerIds)
                {
                    Character selectedCharacter = possibleCharacters.First(character => character.Character_ID == playerId);

                    Encounter_Character tmpChar = new Encounter_Character();
                    tmpChar.Character_ID = selectedCharacter.Character_ID;
                    tmpChar.Encounter_ID = newEncounter.Encounter_ID;
                    tmpChar.Initiative = -1;

                    db.Encounter_Character.Add(tmpChar);
                }

                List<Encounter_Monster> monsterTeam = new List<Encounter_Monster>();
                foreach (int monsterId in monsterIds)
                {
                    Monster selectedMonster = possibleMonsters.First(monster => monster.Monster_ID == monsterId);

                    Encounter_Monster tmpMonster = new Encounter_Monster();
                    tmpMonster.Monster_ID = selectedMonster.Monster_ID;
                    tmpMonster.Encounter_ID = newEncounter.Encounter_ID;
                    tmpMonster.Initiative = -1;

                    db.Encounter_Monster.Add(tmpMonster);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            Encounter.PossibleCharacters = possibleCharacters;
            Encounter.PossibleMonsters = possibleMonsters;

            return View(Encounter);
        }

        //
        // GET: /Map/Edit/5
        /*
        public ActionResult Edit(int id = 0)
        {
            Encounter map = db.Encounters.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Encounter, vmEncounter>(); //auto map entity model to view model
            vmEncounter model = new vmEncounter();
            model = Mapper.Map(map, model);

            model.PossibleCharacters = db.Characters.ToList();
            model.PossibleMonsters = db.Monsters.ToList();

            List<Encounter_Character> playerTeam = db.Encounter_Character.ToList();
            foreach(Encounter_Character player in playerTeam)
            {
                Character tmpChar = db.Characters.Find(player.Character_ID);
                model.PlayerTeam.Add(tmpChar);
            }

            List<Encounter_Monster> monsterTeam = db.Encounter_Monster.ToList();
            foreach (Encounter_Monster monster in monsterTeam)
            {
                Monster tmpMon = db.Monsters.Find(monster.Monster_ID);
                model.MonsterTeam.Add(tmpMon);
            }

            return View(model);


        }

        //
        // POST: /Map/Edit/5

        [HttpPost]
        public ActionResult Edit(vmEncounter model, int[] playerIds, int[] monsterIds)
        {
            List<Character> possibleCharacters = db.Characters.ToList();
            List<Monster> possibleMonsters = db.Monsters.ToList();

            if (ModelState.IsValid)
            {
                Mapper.CreateMap<vmEncounter, Encounter>(); //auto map view model to an existing entity model...
                Encounter Encounter = db.Encounters.Find(model.Encounter_ID);

                Mapper.Map(model, Encounter);

                db.Entry(Encounter).State = EntityState.Modified;
                db.SaveChanges();

                Encounter.Encounter_Character = new List<Encounter_Character>();
                List<Encounter_Character> playerTeam = new List<Encounter_Character>();
                foreach (int playerId in playerIds)
                {
                    Character selectedCharacter = possibleCharacters.First(character => character.Character_ID == playerId);

                    Encounter_Character tmpChar = new Encounter_Character();
                    tmpChar.Character_ID = selectedCharacter.Character_ID;
                    tmpChar.Encounter_ID = Encounter.Encounter_ID;
                    tmpChar.Initiative = "-1";

                    db.Encounter_Character.Add(tmpChar);
                }

                Encounter.Encounter_Monster = new List<Encounter_Monster>();
                List<Encounter_Monster> monsterTeam = new List<Encounter_Monster>();
                foreach (int monsterId in monsterIds)
                {
                    if (db.Encounter_Monster.Find(monsterId, Encounter.Encounter_ID) == null)
                    {
                        Monster selectedMonster = possibleMonsters.First(monster => monster.Monster_ID == monsterId);

                        Encounter_Monster tmpMonster = new Encounter_Monster();
                        tmpMonster.Monster_ID = selectedMonster.Monster_ID;
                        tmpMonster.Encounter_ID = Encounter.Encounter_ID;
                        tmpMonster.Initiative = "-1";

                        db.Encounter_Monster.Add(tmpMonster);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.PossibleCharacters = possibleCharacters;
            model.PossibleMonsters = possibleMonsters;

            return View(model);
        }
        */
        //
        // GET: /Encounter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Encounter Encounter = db.Encounters.Find(id);
            if (Encounter == null)
            {
                return HttpNotFound();
            }
            return View(Encounter);
        }

        //
        // POST: /Encounter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Encounter Encounter = db.Encounters.Find(id);
            db.Encounters.Remove(Encounter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        //Start Encounter
        public ActionResult StartEncounter(int encounterId)
        {
            Encounter map = db.Encounters.Find(encounterId);
            if (map == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Encounter, vmBattle>(); //auto map entity model to view model
            vmBattle model = new vmBattle();
            model = Mapper.Map(map, model);

            List<Encounter_Character> playerTeam = db.Encounter_Character.ToList();
            foreach (Encounter_Character player in playerTeam)
            {
                player.Initiative = this.RollInitative(player.Character.Initative_Bonus);
            }
            model.PlayerTeam = playerTeam;

            List<Encounter_Monster> monsterTeam = db.Encounter_Monster.ToList();
            foreach (Encounter_Monster monster in monsterTeam)
            {
                monster.Initiative = this.RollInitative(0);
            }
            model.MonsterTeam = monsterTeam;

            return View("Battle", model);
        }

        //Advance Turn

        //Log Action


        private short RollInitative(int playerBonus = 0)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int roll = rnd.Next(1, 21);
            for (int i = 0; i > rnd.Next(1, 21); i++)
            {
                roll = rnd.Next(1, 21);
            }

            roll = roll + playerBonus;

            return Convert.ToInt16(roll);
        }
    }
}
