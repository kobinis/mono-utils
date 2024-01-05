using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaUtils;

namespace SolarConflict.XnaUtils
{
    public class FontBank
    {
        private static FontBank bank = null;

        public static FontBank Inst
        {
            get
            {
                if (bank == null)
                {
                    bank = new FontBank();
                }
                return bank;
            }
        }

        public static SpriteFont DefaultFont { get; set; }

        private Dictionary<string, SpriteFont> _spriteBank;

        private FontBank()
        {
            _spriteBank = new Dictionary<string, SpriteFont>();
        }

        public void AddSpriteFont(string id, SpriteFont font)
        {
            _spriteBank.Add(id, font);
            font.Texture.Name = id;            
        }        

        /// <summary>
        /// Try to get SpriteFont from bank, if not found returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpriteFont TryGetSpriteFont(string id)
        {
            if (id == null)
                return null;
            SpriteFont font;
            _spriteBank.TryGetValue(id, out font);
            return font;
        }


    }
}
