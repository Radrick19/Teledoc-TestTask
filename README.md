
# Описание

## Проект запускается на встроенном в Visual Studio Sql сервере, ConnectionString уже заполнен и находится в AppSettings.json

Долго думал получится ли объединить всех клиентов одну сущность и пришёл к выводу, что лучше так не делать. Сейчас в проекте существуют чёткие связи ИП и учредитель ( один к одному ) 
и физ.лицо и учредитель( многие ко многим), мне кажется так архитектура будет надёжней, чем если полагаться на Enum категории и задавать условия через код. 

Также работая над заданием я понял, что некоторую часть кода можно без страха хардкодить - так как у нас всегда будет только 2 вида клиентов ( ИП и физ.лица).
 
На внешний вид времени особо не тратил, так что не судите строго. В остальном если будут какие-то вопросы, можете написать - расскажу почему выбрал то или иное решение.

Также можете посмотреть в пинах моего профиля мои домашние проекты, спасибо за уделённое внимание.
  
## Схема базы данных
 <img src="https://i.imgur.com/29gMhPV.png">

    
