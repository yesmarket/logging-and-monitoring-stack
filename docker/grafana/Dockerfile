#FROM python:alpine3.8 AS build
#
#ARG DEVOPS_BASEURL
#ARG DEVOPS_USERNAME
#ARG DEVOPS_PAT
#ARG _HTTP_PROXY
#
#RUN if [ ! -z "${_HTTP_PROXY}" -a "${_HTTP_PROXY}" != " " ]; then export http_proxy="${_HTTP_PROXY}"; fi
#RUN pip install requests pyyaml
#COPY ./setup.py /setup.py
#RUN python /setup.py

FROM grafana/grafana:5.4.0 AS runtime

ARG GF_INSTALL_PLUGINS

RUN if [ ! -z "${GF_INSTALL_PLUGINS}" ]; then \
    OLDIFS=$IFS; \
        IFS=','; \
    for plugin in ${GF_INSTALL_PLUGINS}; do \
        IFS=$OLDIFS; \
        grafana-cli --pluginsDir "$GF_PATHS_PLUGINS" plugins install ${plugin}; \
    done; \
fi

COPY ./dashboards /etc/grafana/dashboards
#COPY --from=build /etc/grafana/dashboards /etc/grafana/dashboards
COPY ./datasources/prometheus.json /etc/grafana/datasources/prometheus.json
COPY ./import.sh /import.sh
