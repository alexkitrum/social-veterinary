#!/bin/sh
for file in ./migrations/*.dll;
do
  dotnet fm migrate -p mysql --timeout 120 -a $file -c "$CONNECTION_STRING"
done

if [ "$WAIT" -gt 0 ]
then
    sleep infinity
fi
