import sys

from setuptools import setup

if sys.version_info < (3, 6):
    sys.exit("Sorry, Python < 3.6 is not supported")

with open("requirements.txt") as f:
    install_requires = [
        line for line in f.read().split("\n")
        if line and not line.startswith("#")
    ]

setup(
    name="profio python package",
    version="0.1.0",
    author="HutechCJ",
    url="https://github.com/HutechCJ",
    install_requires=install_requires,
)