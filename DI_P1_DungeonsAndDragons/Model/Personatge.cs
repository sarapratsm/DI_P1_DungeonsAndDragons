using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DI_P1_DungeonsAndDragons.Model
{
    public class Personatge : INotifyPropertyChanged
    {
        // Camps privats (backing fields)
        private string nom;
        private Classe classe;
        private Species species;
        private string imageUrl;
        private float altura;
        private float pes;
        private int edat;
        private Alignment _alignment;

        // Llista estàtica
        private static ObservableCollection<Personatge>? llistaPersonatges;

        //Event PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

      

        // CONSTRUCTOR VACÍO (Per a nous personatges)
        public Personatge()
        {
            // Valors per defecte
            PuntsFor = 8;
            PuntsDes = 8;
            PuntsCon = 8;
            PuntsInt = 8;
            PuntsSav = 8;
            PuntsCar = 8;
            Nom = "New Character";
            ImageUrl = "/img/blank.jpg"; // Imatge per defecte
        }

        // CONSTRUCTOR COMPLET
        public Personatge(string nom, Classe classe, Species species, string imageUrl, float altura, float pes, int edat, Alignment _alignment)
        {
            this.nom = nom;
            this.classe = classe;
            this.species = species;
            this.imageUrl = imageUrl;
            this.altura = altura;
            this.pes = pes;
            this.edat = edat;
            this._alignment = _alignment;

            // Valors base per als personatges predefinits
            PuntsFor = 10; PuntsDes = 10; PuntsCon = 10;
            PuntsInt = 10; PuntsSav = 10; PuntsCar = 10;
        }

        // PROPIETATS PÚBLIQUES
        public string Nom { get => nom; set => nom = value; }
        public Classe Classe { get => classe; set => classe = value; }
        public Species Species { get => species; set => species = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public float Altura { get => altura; set => altura = value; }
        public float Pes { get => pes; set => pes = value; }
        public int Edat { get => edat; set => edat = value; }
        public Alignment Alignment { get => _alignment; set => _alignment = value; }

        // Habilitats de classe (Skills)
        public string Skill1 { get; set; }
        public string Skill2 { get; set; }

        // Puntuacions d'Habilitat (Ability Scores)
        public int PuntsFor { get; set; } // Strength
        public int PuntsDes { get; set; } // Dexterity
        public int PuntsCon { get; set; } // Constitution
        public int PuntsInt { get; set; } // Intelligence
        public int PuntsSav { get; set; } // Wisdom
        public int PuntsCar { get; set; } // Charisma

        #region ObservableCollection de personatges

        public static ObservableCollection<Personatge> GetLlistaPersonatges
        {
            get
            {
                if (llistaPersonatges == null)
                {
                    llistaPersonatges = new ObservableCollection<Personatge>();

                    var classes = Classe.GetClasses();
                    var species = Species.GetSpecies();
                    var alignments = Alignment.GetAlignments();

                    // 1. Elzha
                    llistaPersonatges.Add(new Personatge(
                        "Elzha",
                        classes.First(c => c.Name == "Bard"),
                        species.First(s => s.Name == "Tiefling"),
                        "/img/Sample1.jpg",
                        2.10f, 120f, 32,
                        alignments.First(a => a.Name == "Chaotic Good")
                    ));

                    // 2. Elandra
                    llistaPersonatges.Add(new Personatge(
                        "Elandra Moonwhisper",
                        classes.First(c => c.Name == "Druid"),
                        species.First(s => s.Name == "Elf"),
                        "/img/Sample2.jpg",
                        1.70f, 55f, 140,
                        alignments.First(a => a.Name == "Neutral Good")
                    ));

                    // 3. Nyx
                    llistaPersonatges.Add(new Personatge(
                        "Nyx Shadowstep",
                        classes.First(c => c.Name == "Rogue"),
                        species.First(s => s.Name == "Human"),
                        "/img/Sample3.jpg",
                        1.68f, 52f, 27,
                        alignments.First(a => a.Name == "Chaotic Neutral")
                    ));

                    // 4. Lady Aldrena
                    llistaPersonatges.Add(new Personatge(
                        "Lady Aldrena Brightshield",
                        classes.First(c => c.Name == "Paladin"),
                        species.First(s => s.Name == "Human"),
                        "/img/Sample4.jpg",
                        1.75f, 70f, 38,
                        alignments.First(a => a.Name == "Lawful Good")
                    ));

                    // 5. Cobol
                    llistaPersonatges.Add(new Personatge(
                        "Cobol the Silent",
                        classes.First(c => c.Name == "Paladin"),
                        species.First(s => s.Name == "Dragonborn"),
                        "/img/Sample5.jpg",
                        1.83f, 80f, 38,
                        alignments.First(a => a.Name == "Lawful Neutral")
                    ));

                    // 6. Kaleia
                    llistaPersonatges.Add(new Personatge(
                        "Kaleia",
                        classes.First(c => c.Name == "Druid"),
                        species.First(s => s.Name == "Tiefling"),
                        "/img/Sample6.jpg",
                        1.45f, 70f, 112,
                        alignments.First(a => a.Name == "Lawful Good")
                    ));

                    // 7. Zorith
                    llistaPersonatges.Add(new Personatge(
                        "Zorith Malqir",
                        classes.First(c => c.Name == "Bard"),
                        species.First(s => s.Name == "Dwarf"),
                        "/img/Sample7.jpg",
                        1.45f, 70f, 112,
                        alignments.First(a => a.Name == "Lawful Good")
                    ));

                    // 8. Ennerys
                    llistaPersonatges.Add(new Personatge(
                        "Ennerys",
                        classes.First(c => c.Name == "Wizard"),
                        species.First(s => s.Name == "Aasimar"),
                        "/img/Sample8.jpg",
                        1.10f, 35f, 90,
                        alignments.First(a => a.Name == "Neutral Good")
                    ));

                    // 9. Mordek
                    llistaPersonatges.Add(new Personatge(
                        "Mordek Kragthorn",
                        classes.First(c => c.Name == "Warlock"),
                        species.First(s => s.Name == "Gnome"),
                        "/img/Sample9.jpg",
                        1.77f, 68f, 56,
                        alignments.First(a => a.Name == "Neutral Evil")
                    ));

                    // 10. Tara
                    llistaPersonatges.Add(new Personatge(
                        "Tara Greenbottle",
                        classes.First(c => c.Name == "Ranger"),
                        species.First(s => s.Name == "Elf"),
                        "/img/Sample10.jpg",
                        0.98f, 28f, 33,
                        alignments.First(a => a.Name == "Chaotic Good")
                    ));
                }

                return llistaPersonatges;
            }
        }


        #endregion
    }
}