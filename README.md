<div align="center">

<img src="./assets/fsrubik.png" width="256px" height="256px" />

# FsRubik

![License](https://img.shields.io/github/license/reallukee/fsrubik)
![Release](https://img.shields.io/github/v/release/reallukee/fsrubik?include_prereleases)
![Build](https://img.shields.io/github/actions/workflow/status/reallukee/fsrubik/build.yml)

🧩 F#, paradigma funzionale e cubo di Rubik

[Uso](#uso)
•
[Compilazione](#compilazione)
•
[Autore](#autore)
•
[Licenza](#licenza)

</div>



<br />

> [!IMPORTANT]
> **JUST 4 FUN**



# Uso

```fsharp
open Reallukee.FsRubik

let cube = Cube.init ()

Loop.cubeLoop cube

do Console.ReadKey(true)
|> ignore
```



# Compilazione

## 0. Requisiti

### Compilazione

> [!TIP]
> .NET 8.0+ SDK consigliata!

* .NET Core 2.0+ SDK

  *Oppure*

* .NET 5.0+ SDK

### Esecuzione

* .NET Framework 4.6.1+
* .NET Core 2.0+
* .NET 5.0+
* Mono 5.12

## 1. Sorgente

```
git clone https://github.com/reallukee/fsrubik.git
```

## 2. Compila

```
cd fsrubik

dotnet restore fsrubik

dotnet build fsrubik --no-restore --configuration Release
```



# Autore

- [Luca Pollicino](https://github.com/reallukee)



# Licenza

Licenza [MIT](./LICENSE)
