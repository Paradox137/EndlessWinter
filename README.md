# Novel (Демо-проект)

## Визуальная новелла с нодами (развлетвлениями) повествования: начало -> выбор (положительный/отрицательный) -> конец
Управление: нажатием ЛКМ по правой кнопке (область взаимодействия растянута на весь экран), 1ый раз - ускоряется написания текста, 2ой - пишется весь текст сразу, 3ий - новый слайд

## Стек: UniTask, Zenject, UnityCustomEditor(UIElements), UniRx, Patterns, Adressables DOTween, Signals (SignalBus), ChatGPT (GPT3.5, GPT4, NovelGPT), Stable Diffusion

## Коротко про иерархию: 
GameModule:
    Buiseness Module - бизнес логика, StateMachine - для контроля логики и таймлана игры: контроль подгрузки/предзагрузки Adressables новеллы...;
    Collection Module - модуль коллекций
    Configs Module - настройки
    Data Module - модуль данных, их extensions, основные файлы игры, которые сереализуются и десериализуются в JSON 
    Entity Module - модуль entity
    Provider Module - модуль для свзязи сервисов с MVC (предоставление данных через интерфейсы)
    Service Module - сервисы игры
    Storage Module - хранилище загруженных JSON файлов и настроек из Adressables
    UI Module - окна, попапы и MVC как основной паттерн для визуальной новеллы
SharedModule:
    Содержит аналогичные модули + Customize, является более общим и абстрактным от GameModule, можно переиспользовать в других проектах.

## Также:
Adressables: хранение изображений актёров, конфиги глав, конфиги частей глав, JSON текста персонажей, кастомная загрузка, предзагрузка, выгрузка
![image](https://github.com/Paradox137/EndlessWinter/assets/96653165/f5059186-cde4-49ba-958a-16dcd94dff9c)

UIElements: Кастомный Editor Window для удобного создания JSON файлов актёров и сохранением
![image](https://github.com/Paradox137/EndlessWinter/assets/96653165/44f323fd-6e49-456b-a7db-a2c27dc23b61)

UniTask: Как основной инструмент асинхронного взаимодействия в коде, cancellationToken - для контроля и отмены операций

GPT: Для написания диалогов и взаимодействий персонажей по написанным инструкциям.

Stable Diffusion: Для генерации всего арта в игре, изменение эмоций лиц, изменение поз персонажей

UniRx: Для реактивных свойств и реализации основной Observable+Subscribe логики в MVC модуле с шиной событой

![image](https://github.com/Paradox137/EndlessWinter/assets/96653165/fcdcacc2-0dd7-4ae4-b015-f75c8f42b9ac)
![image](https://github.com/Paradox137/EndlessWinter/assets/96653165/be2bc18e-4fde-4672-89d3-ef4593686366)
![image](https://github.com/Paradox137/EndlessWinter/assets/96653165/ceb832eb-fbb0-4a48-af5e-3fa2c72459e9)


