using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTradeHubViewService.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        // Марка

        [ForeignKey("marks")]
        public int MarkaId { get; set; }
        public Marka? Marka { get; set; }

        // Модель

        [ForeignKey("models")]
        public int ModelId { get; set; }
        public Model? Model { get; set; }

        // Поколение

        [ForeignKey("generations")]
        public int GenerationId { get; set; }
        public Generation? Generation { get; set; }

        // Цвет

        [ForeignKey("colors")]
        public int ColorId { get; set; }
        public Color? Color { get; set; }

        // Цена
        public uint Price { get; set; }

        // Объём двигателя
        public float EngineVolume { get; set; }

        // Мощность двигателя
        public ushort EnginePower { get; set; }

        // Расположение руля
        public bool SteeringWheel { get; set; }

        // КПП
        public byte Gearbox { get; set; }

        // Описание
        public string? Description { get; set; }

        // Год
        public ushort Year { get; set; }

        // Тип двигателя
        public byte EngineType { get; set; }

        // Привод
        public byte Privod { get; set; }

        // Тип кузова
        public byte BodyType { get; set; }

        // Пробег
        public uint Probeg { get; set; }
    }
}
