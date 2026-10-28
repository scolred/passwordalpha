# Password Alphabet Highlighter (KeePass 2.58)

**Текущий прототип:** добавляет команду в меню `Tools / Сервис` для открытия отдельного окна с цветной подсветкой пароля. Это пока не кнопка внутри редактора записи.

- Латиница `A-Z`, `a-z` — красная.
- Кириллица `А-Я`, `а-я`, `Ё`, `ё` — зелёная.
- Цифры — белые.
- Остальные символы — светло-серые.

Пароль не изменяется и не записывается обратно в базу. При показе он находится в памяти процесса и отображается в отдельном окне.

## Сборка на Windows

1. Установите Visual Studio Community с workload **.NET desktop development** и .NET Framework 4.8 Targeting Pack.
2. Распакуйте KeePass 2.58 Portable в `C:\Dev\KeePass258`. Убедитесь, что в папке есть `KeePass.exe` и `KeePassLib.dll`.
3. Откройте Developer Command Prompt for Visual Studio.
4. Перейдите в каталог проекта и выполните:

   ```bat
   set KeePassDir=C:\Dev\KeePass258
   build.bat
   ```

5. Результат должен появиться в `bin\Release\PasswordAlphabetHighlighter.dll`.
6. Закройте KeePass, скопируйте DLL в подпапку `Plugins` рядом с `KeePass.exe`, затем запустите KeePass и проверьте `Tools / Сервис`.

## Важно

Это исходный проект, а не готовая скомпилированная DLL. Код привязан к API KeePass 2.x и требует проверки сборкой против конкретного `KeePass.exe` / `KeePassLib.dll`. Если компилятор выдаст ошибку, сохраните полный текст ошибки — по нему можно скорректировать обращение к API.
