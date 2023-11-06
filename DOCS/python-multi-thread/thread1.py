import time
from threading import Thread

def work(id, start, end, result):
    total = 0
    for i in range(start, end):
        total += i
    result.append(total)
    return

if __name__ == "__main__":

    start = time.time()

    result = list()
    th1 = Thread(target=work, args=(1, 0, 100000000, result))
    
    th1.start()
    th1.join()

print(f"Result: { sum(result) }")
print(f"Time: { time.time() - start }")