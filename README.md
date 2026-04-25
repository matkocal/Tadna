# Tadna

**Raw data handler — a scientific tool**

Tadna is an open-source scientific data tool built in C# for field researchers who need simple, no-nonsense data processing. Load your raw field data, enter metadata, and get clean summaries instantly.

---

## Current Features (Phase 1)

- Load bite force CSV files recorded in the field
- Enter animal metadata (species, sex, body mass)
- Display full bite-by-bite table with peak force, duration and timestamp
- Automatic statistics: Max, Min, Mean, N bites

---

## Designed For

Tadna is designed around real field workflows — no internet required, no cloud, no complexity. Bring your SD card home, run Tadna, get your data.

Currently used for bite force measurement in small mammals (*Clethrionomys glareolus*, *Apodemus* sp., *Muscardinus avellanarius*) as part of ongoing wildlife research in Slovakia.

---

## CSV Format

Tadna reads CSV files in the following format, as produced by the companion ESP32 bite force sensor firmware:

```
bite_n,peak_force_N,duration_ms,timestamp_ms
1,45.32,312,4201
2,67.18,280,8540
3,71.02,295,12830
```

---

## How to Run

### Requirements
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)

### Run
```bash
git clone https://github.com/yourusername/Tadna.git
cd Tadna
dotnet run
```

You will be prompted to enter species, sex and body mass before the summary table is displayed.

---

## Output Example

```
Species : Myodes glareolus
Sex     : M
Mass    : 18.3 g

Bite   Peak (N)     Duration (ms)    Timestamp (ms)
----------------------------------------------------
1      45.32        312              4201
2      67.18        280              8540
3      71.02        295              12830
4      68.44        301              17200
5      52.10        288              21500
----------------------------------------------------
Max    71.02
Min    45.32
Mean   60.81
N      5
```

---

## License

MIT — free to use, modify and build upon.

---

*Tadna is named after* Tadorna*, the genus of the shelduck.*
