using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Shared.Libs.Database.DbContextInitializer;


namespace BGP.Games.Database.Modules.Kroko;

public class KrokoWordDbContextSeeder : IDbContextSeeder
{
    private readonly GamesDbContext _context;

    public KrokoWordDbContextSeeder(GamesDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {

        if (_context.KrokoWords.Any()) return;

        await _context.KrokoWords.AddRangeAsync(
        [
            new KrokoWord { LanguageId = 1, Word = "Лес" },
            new KrokoWord { LanguageId = 1, Word = "Жывёла" },
            new KrokoWord { LanguageId = 1, Word = "Сон" },
            new KrokoWord { LanguageId = 1, Word = "Паляна" },
            new KrokoWord { LanguageId = 1, Word = "Краіна" },
            new KrokoWord { LanguageId = 1, Word = "Дзіця" },
            new KrokoWord { LanguageId = 1, Word = "Снег" },
            new KrokoWord { LanguageId = 1, Word = "Піва" },
            new KrokoWord { LanguageId = 1, Word = "Памідор" },
            new KrokoWord { LanguageId = 1, Word = "Лук" },
            new KrokoWord { LanguageId = 1, Word = "Вёска" },
            new KrokoWord { LanguageId = 1, Word = "Неба" },
            new KrokoWord { LanguageId = 1, Word = "Рыба" },
            new KrokoWord { LanguageId = 1, Word = "Чалавек" },
            new KrokoWord { LanguageId = 1, Word = "Брат" },
            new KrokoWord { LanguageId = 1, Word = "Яйка" },
            new KrokoWord { LanguageId = 1, Word = "Сям'я" },
            new KrokoWord { LanguageId = 1, Word = "Прывітанне" },
            new KrokoWord { LanguageId = 1, Word = "Падарунак" },
            new KrokoWord { LanguageId = 1, Word = "Дзякуй" },
            new KrokoWord { LanguageId = 1, Word = "Хлеб" },
            new KrokoWord { LanguageId = 1, Word = "Дом" },
            new KrokoWord { LanguageId = 1, Word = "Рэчанне" },
            new KrokoWord { LanguageId = 1, Word = "Раніца" },
            new KrokoWord { LanguageId = 1, Word = "Чаму" },
            new KrokoWord { LanguageId = 1, Word = "Матка" },
            new KrokoWord { LanguageId = 1, Word = "Добры" },
            new KrokoWord { LanguageId = 1, Word = "Масло" },
            new KrokoWord { LanguageId = 1, Word = "Віно" },
            new KrokoWord { LanguageId = 1, Word = "Мора" },
            new KrokoWord { LanguageId = 1, Word = "Хата" },
            new KrokoWord { LanguageId = 1, Word = "Дэсерт" },
            new KrokoWord { LanguageId = 1, Word = "Свет" },
            new KrokoWord { LanguageId = 1, Word = "Дзед" },
            new KrokoWord { LanguageId = 1, Word = "Рыба" },
            new KrokoWord { LanguageId = 1, Word = "Жыццё" },
            new KrokoWord { LanguageId = 1, Word = "Мужчына" },
            new KrokoWord { LanguageId = 1, Word = "Салата" },
            new KrokoWord { LanguageId = 1, Word = "Груша" },
            new KrokoWord { LanguageId = 1, Word = "Родны" },
            new KrokoWord { LanguageId = 1, Word = "Казка" },
            new KrokoWord { LanguageId = 1, Word = "Сын" },
            new KrokoWord { LanguageId = 1, Word = "Жанчына" },
            new KrokoWord { LanguageId = 1, Word = "Доліна" },
            new KrokoWord { LanguageId = 1, Word = "Вішня" },
            new KrokoWord { LanguageId = 1, Word = "Вечар" },
            new KrokoWord { LanguageId = 1, Word = "Сыр" },
            new KrokoWord { LanguageId = 1, Word = "Морква" },
            new KrokoWord { LanguageId = 1, Word = "Хто" },
            new KrokoWord { LanguageId = 1, Word = "Озера" },
            new KrokoWord { LanguageId = 1, Word = "Бацька" },
            new KrokoWord { LanguageId = 1, Word = "Сок" },
            new KrokoWord { LanguageId = 1, Word = "Дзень" },
            new KrokoWord { LanguageId = 1, Word = "Трава" },
            new KrokoWord { LanguageId = 1, Word = "Смерць" },
            new KrokoWord { LanguageId = 1, Word = "Кніга" },
            new KrokoWord { LanguageId = 1, Word = "Дзе" },
            new KrokoWord { LanguageId = 1, Word = "Любоў" },
            new KrokoWord { LanguageId = 1, Word = "Птушка" },
            new KrokoWord { LanguageId = 1, Word = "Сестра" },
            new KrokoWord { LanguageId = 1, Word = "Часопіс" },
            new KrokoWord { LanguageId = 1, Word = "Молако" },
            new KrokoWord { LanguageId = 1, Word = "Зямля" },
            new KrokoWord { LanguageId = 1, Word = "Дождж" },
            new KrokoWord { LanguageId = 1, Word = "Слова" },
            new KrokoWord { LanguageId = 1, Word = "Агонь" },
            new KrokoWord { LanguageId = 1, Word = "Зорка" },
            new KrokoWord { LanguageId = 1, Word = "Як" },
            new KrokoWord { LanguageId = 1, Word = "Смародзіна" },
            new KrokoWord { LanguageId = 1, Word = "Ветер" },
            new KrokoWord { LanguageId = 1, Word = "Сонца" },
            new KrokoWord { LanguageId = 1, Word = "Бабуля" },
            new KrokoWord { LanguageId = 1, Word = "Ноч" },
            new KrokoWord { LanguageId = 1, Word = "Вода" },
            new KrokoWord { LanguageId = 1, Word = "Напой" },
            new KrokoWord { LanguageId = 1, Word = "Ліст" },
            new KrokoWord { LanguageId = 1, Word = "Агурок" },
            new KrokoWord { LanguageId = 1, Word = "Свята" },
            new KrokoWord { LanguageId = 1, Word = "Кафе" },
            new KrokoWord { LanguageId = 1, Word = "Вода" },
            new KrokoWord { LanguageId = 1, Word = "Веселка" },
            new KrokoWord { LanguageId = 1, Word = "Горад" },
            new KrokoWord { LanguageId = 1, Word = "Гісторыя" },
            new KrokoWord { LanguageId = 1, Word = "Мяса" },
            new KrokoWord { LanguageId = 1, Word = "Суп" },
            new KrokoWord { LanguageId = 1, Word = "Картопля" },
            new KrokoWord { LanguageId = 1, Word = "Часнык" },
            new KrokoWord { LanguageId = 1, Word = "Дочка" },
            new KrokoWord { LanguageId = 1, Word = "Маліна" },
            new KrokoWord { LanguageId = 1, Word = "Горы" },
            new KrokoWord { LanguageId = 1, Word = "Кветка" },
            new KrokoWord { LanguageId = 1, Word = "Рэка" },
            new KrokoWord { LanguageId = 1, Word = "Яблык" },
            new KrokoWord { LanguageId = 1, Word = "Што" },
            new KrokoWord { LanguageId = 1, Word = "Дерева" },
            new KrokoWord { LanguageId = 1, Word = "Дождж" },
            new KrokoWord { LanguageId = 1, Word = "Чай" },
            new KrokoWord { LanguageId = 1, Word = "Буряк" },
        ]);

        await _context.SaveChangesAsync();
    }
}
