#!/bin/bash

set -o errexit
set -o pipefail
set -o nounset
set -o errtrace

javac stilltalt.java

java Solution < test.1.input > test.1.tmp
diff test.1.output test.1.tmp

echo "TEST OK"