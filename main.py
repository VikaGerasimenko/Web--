import json
import sqlite3
import os

JSON_FILE = os.path.join("data", "dilers.json")
DB_FILE = "dealers.db"


def load_dealers(json_path: str):
    with open(json_path, "r", encoding="utf-8") as f:
        return json.load(f)


def create_table(conn: sqlite3.Connection):
    cursor = conn.cursor()
    cursor.execute("DROP TABLE IF EXISTS dealers;")
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS dealers (
            id      INTEGER PRIMARY KEY AUTOINCREMENT,
            name    TEXT    NOT NULL,
            city    TEXT    NOT NULL,
            address TEXT    NOT NULL,
            area    TEXT,
            rating  REAL
        );
    """)
    conn.commit()


def insert_dealers(conn: sqlite3.Connection, dealers: list):
    cursor = conn.cursor()
    cursor.executemany("""
        INSERT INTO dealers (name, city, address, area, rating)
        VALUES (:Name, :City, :Address, :Area, :Rating);
    """, dealers)
    conn.commit()


def main():
    dealers = load_dealers(JSON_FILE)
    print(f"Загружено из JSON: {len(dealers)} дилеров")

    conn = sqlite3.connect(DB_FILE)

    create_table(conn)

    conn.commit()

    insert_dealers(conn, dealers)

    cursor = conn.cursor()
    cursor.execute("SELECT COUNT(*) FROM dealers;")
    count = cursor.fetchone()[0]
    print(f"Вставлено в БД: {count} записей")

    cursor.execute("SELECT id, name, city, rating FROM dealers LIMIT 5;")
    print("\nПервые 5 записей:")
    for row in cursor.fetchall():
        print(row)

    conn.close()
    print(f"\nГотово! База данных: {DB_FILE}")


if __name__ == "__main__":
    main()
