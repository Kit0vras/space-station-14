using System.Text.RegularExpressions;
using Content.Server.SS220.Speech.Components;
using Content.Server.Speech;
using Robust.Shared.Random;

namespace Content.Server.SS220.Speech.EntitySystems;

public sealed class GralnirAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GralnirAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, GralnirAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // c => ск (скажите => сккажите (скккссажите))
        message = Regex.Replace(
            message,
            "с+",
            _random.Pick(new List<string>() { "ск", "скк" })
        );
        // С => CК
        message = Regex.Replace(
            message,
            "С+",
            _random.Pick(new List<string>() { "СК", "СКК" })
        );
        // к => кс (скажите => сккссажите (скккссажите))
        message = Regex.Replace(
            message,
            "к+",
            _random.Pick(new List<string>() { "кс", "ксс" })
        );
        // К => КС
        message = Regex.Replace(
            message,
            "К+",
            _random.Pick(new List<string>() { "КС", "КСС" })
        );
        // ж => шж, шх (пожалуйста => пошхалуйскта, пошжалуйскта)
        message = Regex.Replace(
            message,
            "ж+",
            _random.Pick(new List<string>() { "шж", "шх" })
        );
        // Ж => ШЖ, ШХ
        message = Regex.Replace(
            message,
            "Ж+",
            _random.Pick(new List<string>() { "ШЖ", "ШХ" })
        );
        // ч => ччч
        message = Regex.Replace(
            message,
            "ч+",
            _random.Pick(new List<string>() { "ч", "чч", "ччч" })
        );
        // Ч => ЧЧЧ
        message = Regex.Replace(
            message,
            "Ч+",
            _random.Pick(new List<string>() { "ЧЧ", "ЧЧ", "ЧЧЧ" })
        );

        args.Message = message;
    }
}
