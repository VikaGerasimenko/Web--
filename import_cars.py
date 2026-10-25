import json
import sqlite3
import random

JSON_FILE = "data/cars.json"
DB_FILE = "dealers.db"


def load_cars(json_path):
    with open(json_path, "r", encoding="utf-8") as f:
        data = json.load(f)
    return data["cars"]


def create_cars_table(conn):
    cursor = conn.cursor()
    cursor.execute("DROP TABLE IF EXISTS cars;")
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS cars (
            id         INTEGER PRIMARY KEY AUTOINCREMENT,
            firm       TEXT    NOT NULL,
            model      TEXT    NOT NULL,
            year       INTEGER NOT NULL,
            power      INTEGER NOT NULL,
            color      TEXT,
            price      INTEGER NOT NULL,
            dealer_id  INTEGER NOT NULL,
            FOREIGN KEY (dealer_id) REFERENCES dealers(id)
        );
    """)
    conn.commit()


def get_dealer_ids(conn):
    cursor = conn.cursor()
    cursor.execute("SELECT id FROM dealers ORDER BY id;")
    return [row[0] for row in cursor.fetchall()]


def insert_cars(conn, cars, dealer_ids):
    cursor = conn.cursor()
    rows_to_insert = []
    for car in cars:
        dealer_id = random.choice(dealer_ids)
        rows_to_insert.append((
            car["firm"], car["model"], car["year"],
            car["power"], car["color"], car["price"],
            dealer_id
        ))

    cursor.executemany("""
        INSERT INTO cars (firm, model, year, power, color, price, dealer_id)
        VALUES (?, ?, ?, ?, ?, ?, ?);
    """, rows_to_insert)
    conn.commit()


def main():

    cars = load_cars(JSON_FILE)
    print(f"Загружено из JSON: {len(cars)} машин")

    conn = sqlite3.connect(DB_FILE)
    conn.execute("PRAGMA foreign_keys = ON;")

    create_cars_table(conn)

    conn.commit()

    dealer_ids = get_dealer_ids(conn)
    if not dealer_ids:
        print("Ошибка: нет дилеров. Сначала запусти main.py!")
        conn.close()
        return
    print(f"Найдено дилеров: {len(dealer_ids)}")

    insert_cars(conn, cars, dealer_ids)

    cursor = conn.cursor()
    cursor.execute("SELECT COUNT(*) FROM cars;")
    count = cursor.fetchone()[0]
    print(f"Вставлено в БД: {count} машин")

    conn.close()
    print("Готово!")


if __name__ == "__main__":
    main()