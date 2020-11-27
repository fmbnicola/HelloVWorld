using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Block;


namespace SudoProgram
{
    public class Comparator
    {
        public enum ID
        {
            Equals,
            NotEquals,
            LessThan,
            MoreThan,
            LessEquals,
            MoreEquals
        }

        public ID Id { get; protected set; }


        public Comparator(ID id)
        {
            this.Id = id;
        }


        public bool Compare(Value one, Value two)
        {
            switch (this.Id)
            {
                case Comparator.ID.Equals:
                    if (one.Id == two.Id) return true;
                    break;

                case Comparator.ID.NotEquals:
                    if (one.Id != two.Id) return true;
                    break;

                case Comparator.ID.LessThan:
                    if (one.Id < two.Id) return true;
                    break;

                case Comparator.ID.MoreThan:
                    if (one.Id > two.Id) return true;
                    break;

                case Comparator.ID.LessEquals:
                    if (one.Id <= two.Id) return true;
                    break;

                case Comparator.ID.MoreEquals:
                    if (one.Id >= two.Id) return true;
                    break;
            }

            return false;
        }
    }
}
